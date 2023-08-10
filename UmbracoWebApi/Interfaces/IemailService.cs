namespace EmailSenderApp.Site
{
    public interface IemailService
    {
        public bool sendEmail(string userName, string message, string subject);    
    }
}