using NHISWeb.Models.Entities;

namespace NHISWeb.ViewModels
{
    public class RegisterRequest
    {
        public string Password { get; set; }
        public User User { get; set; }
    }
}
