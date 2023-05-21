using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHISWeb.Models.AuthorizationCode
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
    public class VerifyAuthorizationCodeDto
    {
        [Required(ErrorMessage = "Enter authorization code")]
        public string InputCode { get; set; }
        //If verified, write a query to fetch all data from DB
    }
}
