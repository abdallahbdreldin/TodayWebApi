using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace TodayWebApi.BLL.Managers
{
    public class EmailManager : IEmailManager
    {
        private readonly IConfiguration _config;

        public EmailManager(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var apiKey = _config["SendGridSettings:ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("SendGrid API Key is missing. Please configure it in appsettings.json.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_config["SendGridSettings:FromEmail"], _config["SendGridSettings:FromName"]);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                throw new Exception($"Failed to send email. Status Code: {response.StatusCode}. Response: {errorBody}");
            }
        }
    }
}
