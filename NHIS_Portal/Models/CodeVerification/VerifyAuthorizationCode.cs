using System.ComponentModel.DataAnnotations;

namespace NHIS_Portal.Models.CodeVerification
{
    public class VerifyAuthorizationCode
    {
        [Required(ErrorMessage = "Enter authorization code")]
        public string? InputCode { get; set; }
        //If verified, write a query to fetch all data from DB
    }
}
