using System.ComponentModel.DataAnnotations;

namespace NHISWeb.Views.VerifyCodeModel
{
    public class VerifyAuthCodeClass
    {
        [Required(ErrorMessage = "Enter authorization code")]
       public string EnterAuthorisationCode { get; set; }
    }
}
