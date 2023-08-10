using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace EmailSenderApp.Site.umbraco.Repository
{
    public class EmailRepo : IemailService
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<EmailRepo> _log;

		public EmailRepo(IConfiguration config, ILogger<EmailRepo> logger)
		{
			_configuration = config;
			_log = logger;
		}
        public bool sendEmail(string userEmail, string message, string subject)
        {
			bool result = false;
			try
			{
				_log.LogInformation($"Sending Message from {userEmail}...");
				string senderEmail = _configuration.GetSection("UserEmailCredentials")["userEmail"];
				string recieverEmail = _configuration.GetSection("UserEmailCredentials")["recieverEmail"];
				string userPassowrd = _configuration.GetSection("UserEmailCredentials")["userPassword"];

				SmtpClient client = new SmtpClient();
				client.Port = 587;//25;
				client.Host = "smtp.gmail.com"; //"192.168.171.18";
				client.Timeout = 100000;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential(senderEmail, userPassowrd);
				client.EnableSsl = true;

				MailMessage mm = new MailMessage(senderEmail, recieverEmail, subject, message + $"\n Email: {userEmail}");
				mm.IsBodyHtml = false;
				mm.BodyEncoding = UTF8Encoding.UTF8;
				mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

								
				client.Send(mm);
				_log.LogInformation($"Email Sent to: {userEmail}...");
				result = true;
			}
			catch (Exception ex)
			{
				_log.LogError(ex.ToString());
			}
			return result;
		}
    }
}
