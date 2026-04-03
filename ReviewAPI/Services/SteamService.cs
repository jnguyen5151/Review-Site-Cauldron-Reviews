using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReviewAPI.DTOs.Steam;
using ReviewAPI.Models;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReviewAPI.Services
{
    public class SteamService
    {
        private readonly HttpClient _http;
        private readonly SteamSettings _steamSettings;
        private readonly GameReviewContext _context;

        public SteamService(HttpClient http, IOptions<SteamSettings> steamSettings, GameReviewContext context)
        {
            _http = http;
            _steamSettings = steamSettings.Value;
            _context = context;
        }

        public async Task ImportAppsBase()
        {

            for (int page = 0; page <= 9; page++)
            {
                Console.WriteLine($"Featching Page {page}");
                await TestSteamList(page);

                if (page < 9)
                {
                    Console.WriteLine("On Cooldown...");
                    await Task.Delay(TimeSpan.FromSeconds(65));
                }
            }
        }

        public async Task TestSteamList(int page)
        {
            var data = await _http.GetFromJsonAsync<Dictionary<string, SteamAppDto>>($"https://steamspy.com/api.php?request=all&page={page}");
            if (data == null) return;

            var apps = new List<SteamApp>();
            var existingIds = await _context.SteamApps.Select(a => a.AppId).ToHashSetAsync();
            var existingDevelopers = await _context.Developers.ToDictionaryAsync(d => d.Developer, StringComparer.OrdinalIgnoreCase);
            var existingPublishers = await _context.Publishers.ToDictionaryAsync(p => p.Publisher, StringComparer.OrdinalIgnoreCase);
            var newDevs = new List<SteamAppDeveloper>();
            var newPubs = new List<SteamAppPublisher>();

            foreach (var dto in data.Values)
            {
                if (existingIds.Contains(dto.AppId)) continue;

                Console.WriteLine($"Sample - Name: {dto.Name}, Devs: '{dto.Developer}', Pubs: '{dto.Publisher}'");

                var parts = dto.Owners.Split("..");
                var min = int.Parse(parts[0].Replace(",", "").Trim());
                var max = int.Parse(parts[1].Replace(",", "").Trim());
                var avg = (max + min) / 2;

                var app = new SteamApp
                {
                    AppId = dto.AppId,
                    Name = dto.Name,
                    Price = dto.InitialPrice / 100m,
                    OwnersMax = max,
                    OwnersMin = min,
                    OwnersAvg = avg
                };

                var devs = SplitCompanyNames(dto.Developer).Distinct();

                foreach (var devName in devs)
                {
                    if (!existingDevelopers.TryGetValue(devName, out var devRow))
                    {
                        devRow = new SteamAppDeveloper { Developer = devName };
                        existingDevelopers[devName] = devRow;
                        newDevs.Add(devRow);
                    }
                }

                var pubs = SplitCompanyNames(dto.Publisher).Distinct();

                foreach (var pubName in pubs)
                {
                    if(!existingPublishers.TryGetValue(pubName, out var pubRow))
                    {
                        pubRow = new SteamAppPublisher { Publisher = pubName };
                        existingPublishers[pubName] = pubRow;
                        newPubs.Add(pubRow);
                    }
                }

                apps.Add(app);
            }

            await _context.AddRangeAsync(newDevs);
            await _context.AddRangeAsync(newPubs);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            existingDevelopers = await _context.Developers.ToDictionaryAsync(d => d.Developer, StringComparer.OrdinalIgnoreCase);
            existingPublishers = await _context.Publishers.ToDictionaryAsync(p => p.Publisher, StringComparer.OrdinalIgnoreCase);

            await _context.SteamApps.AddRangeAsync(apps);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var devJoins = new List<SteamAppToDeveloper>();
            var pubJoins = new List<SteamAppToPublisher>();


            foreach (var dto in data.Values)
            {
                if (!apps.Any(a => a.AppId == dto.AppId)) continue;

                var devs = SplitCompanyNames(dto.Developer).Distinct();

                foreach (var devName in devs)
                {
                    if (existingDevelopers.TryGetValue(devName, out var devRow))
                        devJoins.Add(new SteamAppToDeveloper { AppId = dto.AppId, DeveloperId = devRow.DeveloperId });
                }

                var pubs = SplitCompanyNames(dto.Publisher).Distinct();

                foreach (var pubName in pubs)
                {
                    if (existingPublishers.TryGetValue(pubName, out var pubRow))
                        pubJoins.Add(new SteamAppToPublisher { AppId = dto.AppId, PublisherId = pubRow.PublisherId });
                }
            }

            await _context.AppDevelopers.AddRangeAsync(devJoins);
            await _context.AppPublishers.AddRangeAsync(pubJoins);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

        }

        public static readonly HashSet<string> _suffixes = new(StringComparer.OrdinalIgnoreCase)
        {
            "Inc.", "Inc", "LLC.", "LLC", "Ltd.", "Ltd", "Co.", "Co", "Corp.", "Corp", "GmbH", "S.A.", "S.L."
        };

        public IEnumerable<string> SplitCompanyNames(string input)
        {

            if (string.IsNullOrWhiteSpace(input)) 
            {
                return Enumerable.Empty<string>();
            }

            var parts = input.Split(',').Select(p => p.Trim()).ToList();

            var result = new List<string>();
            var current = parts[0];

            for (int i = 1; i < parts.Count; i++)
            {
                if (_suffixes.Contains(parts[i]))
                    current += ", " + parts[i];
                else
                {
                    result.Add(current);
                    current = parts[i];
                }
            }

            result.Add(current);
            return result.Where(r => !string.IsNullOrWhiteSpace(r));
        }

        public async Task TestSteamDetails()
        {
            int batchSize = 200;

            var appList = new List<SteamApp>();

            do
            {
                appList = await _context.SteamApps
                    .Where(a => !a.IsEnriched)
                    .Take(batchSize)
                    .ToListAsync();

                var existingGenres = await _context.Genres
                    .ToDictionaryAsync(d => d.GenreId);
                var existingCategory = await _context.Categories
                    .ToDictionaryAsync(c => c.CategoryId);

                var newCategories = new List<SteamAppCategory>();
                var newGenres = new List<SteamAppGenre>();

                var responses = new Dictionary<int, SteamStoreDataDto>();

                foreach (var app in appList)
                {
                    var response = await _http.GetFromJsonAsync<Dictionary<string, SteamStoreDto>>($"https://store.steampowered.com/api/appdetails?appids={app.AppId}");
                    var steamDto = response?[app.AppId.ToString()];
                
                    if (!steamDto!.Success)
                    {
                        Console.WriteLine("UNSUCCESSFUL");
                        continue;
                    }

                    SteamStoreDataDto data = steamDto.Data;
                    responses[app.AppId] = data;

                    app.Type = data.Type;
                    app.RequiredAge = data.RequiredAge;
                    app.IsFree = data.IsFree;
                    app.Description = data.ShortDescription;
                    app.HeaderImage = data.CardImage;
                    app.Windows = data.Platforms!.Windows;
                    app.Mac = data.Platforms.Mac;
                    app.Linux = data.Platforms.Linux;
                    app.ReleaseDate = data.ReleaseDate!.Date;
                    app.IsEnriched = true;

                    foreach(CategoryDto dto in data.Category)
                    {
                        if (!existingCategory.TryGetValue(dto.Id, out var categoryRow))
                        {
                            categoryRow = new SteamAppCategory { CategoryId = dto.Id, Category = dto.Category };
                            existingCategory[dto.Id] = categoryRow;
                            newCategories.Add(categoryRow);
                        }
                    }

                    foreach(GenreDto dto in data.Genre)
                    {
                        if(!existingGenres.TryGetValue(dto.Id, out var genreRow))
                        {
                            genreRow = new SteamAppGenre { GenreId = dto.Id, Genre = dto.Genre };
                            existingGenres[dto.Id] = genreRow;
                            newGenres.Add(genreRow);
                        }
                    }
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }

                await _context.AddRangeAsync(newCategories);
                await _context.AddRangeAsync(newGenres);
                _context.SteamApps.UpdateRange(appList);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();

                existingGenres = await _context.Genres
                    .ToDictionaryAsync(d => d.GenreId);
                existingCategory = await _context.Categories
                    .ToDictionaryAsync(c => c.CategoryId);

                var genreJoins = new List<SteamAppToGenre>();
                var categoryJoins = new List<SteamAppToCategory>();

                foreach (var app in appList) 
                {
                    if (!responses.TryGetValue(app.AppId, out var data)) continue;

                    foreach (CategoryDto dto in data.Category)
                    {
                        if (existingCategory.TryGetValue(dto.Id, out var categoryRow))
                        {
                            categoryJoins.Add(new SteamAppToCategory { AppId = app.AppId, CategoryId = categoryRow.CategoryId });
                        }
                    }

                    foreach (GenreDto dto in data.Genre)
                    {
                        if (existingGenres.TryGetValue(dto.Id, out var genreRow))
                        {
                            genreJoins.Add(new SteamAppToGenre { AppId = app.AppId, GenreId = genreRow.GenreId });
                        }
                    }
                }

                await _context.AppCategories.AddRangeAsync(categoryJoins);
                await _context.AppGenres.AddRangeAsync(genreJoins);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();

            } while (appList.Count == batchSize);

        }
    }
}
