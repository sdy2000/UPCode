using DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(20, ErrorMessage = "{0} cannot be greater than {1}!")]
        [MinLength(6, ErrorMessage = "{0} cannot be less than {1}!")]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Passwords are inconsistent!")]
        [Display(Name = "RePassword")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string RePassword { get; set; }


        [Display(Name = "Terms and Conditions")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public bool Rules { get; set; }
    }
    public class IsRegisterViewModel
    {
        public bool IsExistUserName { get; set; }
        public bool IsExistEmail { get; set; }
        public bool IsAddUser { get; set; }
        public bool IsSendActiovationEmail { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class LoginViewModel
    {

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(20, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }


    public class ForgotPasswordViewModel
    {

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string Email { get; set; }
    }
    public class ResetPasswordVeiwModel
    {
        public string ActiveCode { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(20, ErrorMessage = "{0} cannot be greater than {1}!")]
        [MinLength(6, ErrorMessage = "{0} cannot be less than {1}!")]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Passwords are inconsistent!")]
        [Display(Name = "RePassword")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string RePassword { get; set; }
    }
}
