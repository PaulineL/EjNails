using Infrastructure.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SiteJu.Helpers
{
    public class IdentityMailSender : IEmailSender
    {
        private IMailSender _mailSender;
        private IConfiguration _configuration;

        public IdentityMailSender(IMailSender mailSender, IConfiguration configuration)
        {
            _mailSender = mailSender;
            _configuration = configuration;

        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return _mailSender.SendMail(email, _configuration["SiteConfiguration:CompanyName"], htmlMessage, subject);
        }
    }
}
