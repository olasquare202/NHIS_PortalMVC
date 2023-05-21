using Microsoft.AspNetCore.Identity;
using NHISWeb.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHISWeb.Dto.RequestDto
{
    public class UserRegister 
    {
        public int StaffId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        
        public int BranchId { get; set; }
        public int UserRoleId { get; set; }
       
        
        
    }
}
