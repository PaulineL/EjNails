using System;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Mail
{
	public class SendgridMailSender : IMailSender
	{
        private readonly IOptions<MailOption> _mailOptions;
        private readonly ISendGridClient _sendgridClient;

        public SendgridMailSender(IOptions<MailOption> mailOptions, ISendGridClient sendgridClient)
		{
            _mailOptions = mailOptions;
            _sendgridClient = sendgridClient;
        }

        public async Task<bool> SendMail(string recipient, string displayName, string content)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_mailOptions.Value.Sender),
                PlainTextContent = content,
                HtmlContent = $"<p>{content}</p>",
                Subject = $"[Web] Prise de contact : {displayName}",
                ReplyTo = new EmailAddress(recipient, $"{displayName}")
            };
            msg.AddTo(new EmailAddress(_mailOptions.Value.Contact));

            var response = await _sendgridClient.SendEmailAsync(msg);
            var sendgridResponse = response.Body.ReadAsStringAsync().Result;

            return string.IsNullOrEmpty(sendgridResponse);
        }
    }

    public class SendgridOption
    {
        public string ApiKey { get; set; }
    }
}

