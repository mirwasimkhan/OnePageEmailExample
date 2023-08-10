using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;
using System.Text;
using UmbracoWebApi.Models;

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
        public bool sendEmail(User user)
        {
			bool result = false;
			try
			{
				if (!Validate(user))
				{
					_log.LogInformation($"Sending Message from {user.email}...");
					string senderEmail = _configuration.GetSection("UserEmailCredentials")["userEmail"];
					string userPassowrd = _configuration.GetSection("UserEmailCredentials")["userPassword"];
					string emailFooter = _configuration.GetSection("EmailFooter")["footerValue"];
					string recieverEmail = user.email;

					SmtpClient client = new SmtpClient();
					client.Port = 587;//25;
					client.Host = "smtp.gmail.com"; //"192.168.171.18";
					client.Timeout = 100000;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.UseDefaultCredentials = false;
					client.EnableSsl = true;
					client.Credentials = new NetworkCredential(senderEmail, userPassowrd);
					client.EnableSsl = true;
					StringBuilder sb = new StringBuilder();
					sb.Append($"Message By: {user.name}\n Email: {user.messageFrom}");
					sb.Append(user.message);
					sb.Append($"\n\n\n\n {emailFooter}");

					MailMessage mm = new MailMessage(senderEmail, recieverEmail, user.subject, sb.ToString());
					mm.IsBodyHtml = false;
					mm.BodyEncoding = UTF8Encoding.UTF8;
					mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


					client.Send(mm);
					_log.LogInformation($"Email Sent to: {user.email}...");
					result = true;
				}
			}
			catch (Exception ex)
			{
				_log.LogError(ex.ToString());
			}
			return result;
		}
		private bool Validate(User user)
		{
			return (string.IsNullOrEmpty(user.email) || string.IsNullOrEmpty(user.subject) || string.IsNullOrEmpty(user.message) ||
				string.IsNullOrEmpty(user.messageFrom));
		}
    }
}
