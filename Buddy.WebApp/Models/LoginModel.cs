using System.ComponentModel.DataAnnotations;
using Buddy.Translations;

namespace Buddy.WebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Username", ResourceType = typeof(Translation))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Password", ResourceType = typeof(Translation))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Translation))]
        public bool RememberMe { get; set; }
    }

    public class TwoFactorModel
    {
        [Display(Name = "Username", ResourceType = typeof(Translation))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "TwoFactorToken", ResourceType = typeof(Translation))]
        public string TwoFactorToken { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Translation))]
        public bool RememberMe { get; set; }

        [Display(Name = "ReturnUrl", ResourceType = typeof(Translation))]
        public string ReturnUrl { get; set; }

        [Display(Name = "BarcodeImageUrl", ResourceType = typeof(Translation))]
        public string BarcodeImageUrl { get; set; }

        [Display(Name = "SetupCode")]
        public string SetupCode { get; set; }
    }
}