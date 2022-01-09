using System;
namespace Infrastructure.Mail
{
    public class MailOption
    {
        public string Contact { get; set; }
        public string Sender { get; set; }
        public string Provider { get; set; }

        public SendgridOption Sendgrid { get; set; }
        public MicrosoftGraphOption MSGraph { get; set; }
    }
}

