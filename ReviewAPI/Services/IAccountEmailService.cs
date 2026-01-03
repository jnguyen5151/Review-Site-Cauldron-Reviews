namespace ReviewAPI.Services
{
    public interface IAccountEmailService
    {
        Task SendVerificationEmailAsync(string email, string verificationLink);
    }
}
