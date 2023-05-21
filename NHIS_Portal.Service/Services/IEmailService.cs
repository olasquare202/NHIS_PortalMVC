using NHIS_Portal.Service.Models;


namespace NHIS_Portal.Service.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
