using System.ComponentModel.DataAnnotations;

namespace NHISWeb.Models.Authentication
{
    public class Login
    {
        [Required(ErrorMessage = "Your email is required")]
        //[DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Kindly enter your password")]
        public string? Password { get; set; }
    }
}
