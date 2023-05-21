using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHIS_Portal.Models.AuthorizationCode
{
    public class AuthorizationCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EnroleeName { get; set; }
        public string EnroleeNumber { get; set; }
        public string EnroleePhoneNumber { get; set; }
        public string Provider { get; set; }
        public string Diagnosis { get; set; }
        public string? IssuedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string Code { get; set; }
    }
}
