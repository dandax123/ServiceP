
using System.ComponentModel.DataAnnotations;

namespace ServiceP.Auth
{

    public class BaseRegistrationRequest 
    {
        [Required(ErrorMessage = "Provide your password")]
        public string password {get; set;}

        [Required(ErrorMessage = "Provide your email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Provide your First Name")]
        public string first_name  {get; set;}

        [Required(ErrorMessage = "Provide your Last Name")]
        public string last_name {get; set;}
        


    }

    public class LoginRequest
    {
        [Required(ErrorMessage ="Provide your password")]
        public string password { get; set; }
        
        [Required(ErrorMessage = "Provide your email")]
        public string email { get; set; }
    }

    public class LoginResponse
    {
           public string token { get; set; }
    }
    public class ProviderRegistrationRequest : BaseRegistrationRequest
    {
        [Required(ErrorMessage = "Provide your Service Brand name")]
        public string brand_name { get; set; }
    }
}
