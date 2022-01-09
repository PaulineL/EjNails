using System;
namespace Infrastructure.Mail
{
	public interface IMailSender
	{
		public Task<bool> SendMail(string recipient, string senderDisplayName, string content);
	}
}

