namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Mail;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class EmailService : IEmailService
	{
		public void SendEmail(string to, string subject, string body)
		{
			SmtpClient client = new SmtpClient();
			MailMessage message = new MailMessage();

			message.To.Add(new MailAddress(to));
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = false;

			client.Send(message);
		}
	}
}
