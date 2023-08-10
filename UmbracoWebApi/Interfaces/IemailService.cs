using UmbracoWebApi.Models;

namespace EmailSenderApp.Site
{
    public interface IemailService
    {
        public bool sendEmail(User user);
    }
}