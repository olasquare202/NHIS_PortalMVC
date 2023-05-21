using System.ComponentModel.DataAnnotations;

namespace NHIS_Portal.Models.Authentication.SignUp
{
    public class ResetPassword
    {
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
        public string Email { get; set; }
        public string Token { get; set; } = null!;
    }
}
