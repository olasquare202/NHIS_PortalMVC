using System.ComponentModel.DataAnnotations;

namespace NHISWeb.Models.Authentication.SignUp
{
    public class Register
    {
        public int BranchId { get; set; }

        public int DepartmentId { get; set; }
        public int UserRoleId { get; set; }
        public int StaffId { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
    }
}
