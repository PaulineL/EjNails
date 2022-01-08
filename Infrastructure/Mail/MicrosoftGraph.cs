using System;
using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace Infrastructure.Mail
{
	public class MicrosoftGraph : IMailSender
	{
        private readonly IOptions<MailOption> _mailOptions;
        private GraphHelper _graphHelper;
        private string _token;

        public MicrosoftGraph(IOptions<MailOption> mailOptions) 
		{
            _mailOptions = mailOptions;
        }

        public async Task<bool> SendMail(string recipient, string senderDisplayName, string content)
        {
            throw new NotImplementedException();
        }

        public class GraphHelper
        {
            private static DeviceCodeCredential tokenCredential;
            private static GraphServiceClient graphClient;

            public static void Initialize(string clientId,
                                          string[] scopes,
                                          Func<DeviceCodeInfo, CancellationToken, Task> callBack)
            {
                tokenCredential = new DeviceCodeCredential(callBack, clientId);
                graphClient = new GraphServiceClient(tokenCredential, scopes);
            }

            public static async Task<string> GetAccessTokenAsync(string[] scopes)
            {
                var context = new TokenRequestContext(scopes);
                var response = await tokenCredential.GetTokenAsync(context);
                return response.Token;
            }
        }

    }

    public class MicrosoftGraphOption
    {
        public string ApplicationId { get; set; }
    }
}

