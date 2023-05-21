using System.ComponentModel.DataAnnotations.Schema;

namespace NHIS_Portal.Models.AuthorizationCode
{
    public class CreateAuthorizationCode
    {
        //public int AuthorizationCodeId { get; set; }
        public string EnroleeName { get; set; }
        public string EnroleeNumber { get; set; }
        public string EnroleePhoneNumber { get; set; }
        public string Provider { get; set; }
        public string Diagnosis { get; set; }
        //public string Code { get; set; }

    }
}
