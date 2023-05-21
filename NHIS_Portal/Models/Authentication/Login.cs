using System.ComponentModel.DataAnnotations;

namespace NHIS_Portal.Models.Authentication
{
    public class Login
    {
        [Required(ErrorMessage = "Your email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Kindly enter your password")]
        public string? Password { get; set; }
    }
}
