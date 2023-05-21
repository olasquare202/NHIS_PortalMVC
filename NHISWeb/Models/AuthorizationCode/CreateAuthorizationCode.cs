namespace NHISWeb.Models.AuthorizationCode
{
    public class CreateAuthorizationCode
    {
        
        public string EnroleeName { get; set; }
        public string EnroleeNumber { get; set; }
        public string EnroleePhoneNumber { get; set; }
        public string Provider { get; set; }
        public string Diagnosis { get; set; }
        
    }
}

