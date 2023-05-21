using System.ComponentModel.DataAnnotations;

namespace NHISWeb.Dto.RequestDto
{
    public class UserLogin
    {
        //[Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
