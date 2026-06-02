using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace ReviewAPI.Services
{
    public class AccountEmailService : IAccountEmailService
    {
        private readonly IAmazonSimpleEmailService _ses;
        private readonly IConfiguration _config;

        public AccountEmailService(
            IAmazonSimpleEmailService ses,
            IConfiguration config)
        {
            _ses = ses;
            _config = config;
        }

        public async Task SendVerificationEmailAsync(string email, string verificationLink)
        {
            var fromEmail = _config["SES:FromEmail"];

            if (string.IsNullOrEmpty(fromEmail)) 
            {
                throw new InvalidOperationException("Could not get SES:FromEmail");
            }

            var request = new SendEmailRequest
            {
                Source = fromEmail,
                Destination = new Destination
                {
                    ToAddresses = new List<string> { email }
                },
                Message = new Message
                {
                    Subject = new Content("Verify your Account"),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = $@"
                                <!DOCTYPE html>
                                <html>
                                  <body>
                                    <p>Confirm your Account with TheCauldron Reviews</p>
                                    <p>If you did not create an account, you can safely ignore this email.</p>
                                    <p>
                                      <a href=""{verificationLink}"">
                                        Click here to verify your account
                                      </a>
                                    </p>
                                    <p>
                                        If the link is unaccessible, please copy the following link into the browser:
                                    </p>
                                    <p>
                                        {verificationLink}
                                    </p>
                                  </body>
                                </html>"
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data =
                                $@"Verify your account by visiting this link:
                                {verificationLink}"
                        }
                    }
                }
            };
            await _ses.SendEmailAsync(request);
        }
    }
}
