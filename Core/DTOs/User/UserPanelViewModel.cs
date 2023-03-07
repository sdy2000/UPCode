using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class InformationUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? UesrGender { get; set; }
        public string RegisterDate { get; set; }
        public string PhonNumber { get; set; }
    }

    public class SideBarUserPanelViewModel
    {
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string RegisterDate { get; set; }
    }

    public class EditProfileViewModel
    {
        [Display(Name = "Email")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? Email { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(70, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? FirstName { get; set; }


        [Display(Name = "Last Name")]
        [MaxLength(70, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? LastName { get; set; }


        [Display(Name = "Phone Number")]
        [MaxLength(15, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? PhonNumber { get; set; }



        [Display(Name = "Gender")]
        public int? GenderId { get; set; }

        [Display(Name = "User Avatar")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string UserAvatar { get; set; }


        public IFormFile? UserImage { get; set; }
    }

    public class EditProfileInfoViewModel
    {
        public bool IsEditProfile { get; set; }
        public bool IsEmailExist { get; set; }
        public bool IsSendActiveEmail { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ChengePassword
    {
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string OldPassword { get; set; }



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
