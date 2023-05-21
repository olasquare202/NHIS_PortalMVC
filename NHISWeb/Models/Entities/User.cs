using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHISWeb.Models.Entities
{
    public class User : IdentityUser
    {
        
        public int StaffId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        //navigation property
        [ForeignKey("UserRoleId")]
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        //One to one relationship
        [ForeignKey("BranchId")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        //public string SuccessMessage { get; internal set; }
    }
}
