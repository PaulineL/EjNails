using System;
using System.Net.Http.Headers;
using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Infrastructure.Mail
{
	public class MicrosoftGraph : IMailSender
	{
        private readonly ClientSecretCredential _clientSecretCredential;
        private readonly IOptions<MailOption> _mailOptions;

        public MicrosoftGraph(IOptions<MailOption> mailOptions) 
		{
            _mailOptions = mailOptions;
            _clientSecretCredential = new ClientSecretCredential(
                _mailOptions.Value.MSGraph.TenantId,
                _mailOptions.Value.MSGraph.ClientId,
                _mailOptions.Value.MSGraph.ClientSecret);
        }

        public async Task<bool> SendMail(string recipient, string senderDisplayName, string content, string subject)
        {
            var sender = new GraphServiceClient(_clientSecretCredential).Users[_mailOptions.Value.MSGraph.UserSenderId];
            var sendResult = await sender.SendMail(new Message
            {
                Subject = subject,
                Body = new ItemBody() { Content = $"<p>{content}</p>", ContentType = BodyType.Html },
                ToRecipients = new List<Recipient> { new Recipient { EmailAddress = new EmailAddress { Address = _mailOptions.Value.Contact } } },
                ReplyTo = new List<Recipient> { new Recipient { EmailAddress = new EmailAddress { Address = recipient, Name = senderDisplayName } } }
            })
                .Request()
                .PostResponseAsync();

            var result = await sendResult.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(result);
        }
    }

    public class MicrosoftGraphOption
    {
        public string ApplicationId { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UserSenderId { get; set; }
    }
}

