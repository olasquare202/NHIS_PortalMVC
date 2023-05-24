using System.ComponentModel.DataAnnotations;

namespace NHISWeb.Dto.ResponseDto
{
    public class VerifyAuthorizationCodeDto
    {
        [Required(ErrorMessage = "Enter authorization code")]
        public string EnterAuthorisationCode { get; set; }
        //If verified, write a query to fetch all data below from DB
        public string EnroleeName { get; set; }
        public string EnroleeNumber { get; set; }
        public string EnroleePhoneNumber { get; set; }
        public string Provider { get; set; }
        public string Diagnosis { get; set; }
        public string IssuedBy { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Code { get; set; }
    }
}



