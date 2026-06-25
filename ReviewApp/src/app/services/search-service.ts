import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { CardModel, GenreModel, CategoryModel, PublisherModel, DeveloperModel } from '../models/game-search';

@Injectable({
  providedIn: 'root',
})
export class SearchService {

  private url = `${environment.apiUrl}/api/SteamApp`;
  private http = inject(HttpClient);

  gameSearch(options: {
    search?: string;
    categories?: number[];
    genres?: string[];
    publishers?: string[];
    developers?: string[];
    steamAppId?: number;
    page?: number;
    appCount?: number;
  }): Observable<CardModel[]> {
    let params = new HttpParams();
    const { search, categories, genres, publishers, developers, steamAppId, page, appCount } = options;

    if (search) params = params.set('search', search);
    if (categories) categories.forEach(c => params = params.append('categories', c.toString()));
    if (genres) genres.forEach(g => params = params.append('genres', g.toString()));
    if (publishers) publishers.forEach(p => params = params.append('publishers', p.toString()));
    if (developers) developers.forEach(d => params = params.append('developers', d.toString()));
    if (steamAppId) params = params.set('steamAppId', steamAppId);
    if (page) params = params.set('page', page);
    if (appCount) params = params.set('appCount', appCount);
    
    return this.http.get<CardModel[]>(`${this.url}/search`, { params });
  }

}
