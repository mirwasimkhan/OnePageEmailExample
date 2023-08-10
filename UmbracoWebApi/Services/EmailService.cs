namespace EmailSenderApp.Site.umbraco.Services
{
    public class EmailService
    {
        private readonly IemailService _emailService;

        public EmailService(IemailService emailService)
        {
            _emailService = emailService;
        }

        public bool SendEmail(string email, string subject, string message) 
        { 
            return _emailService.sendEmail(email, subject, message);
        }
    }
}
