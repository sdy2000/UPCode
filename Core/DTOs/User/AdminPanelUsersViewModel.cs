using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.User
{
    public class UserForAdminViewModel
    {
        public List<DataLayer.Entities.User.User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int UserCount { get; set; }
    }

    public class CreateUserForAdminViewModel
    {
        [Display(Name = "Gender")]
        public int? GenderId { get; set; }


        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(30, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(150, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(20, ErrorMessage = "{0} cannot be greater than {1}!")]
        [MinLength(6, ErrorMessage = "{0} cannot be less than {1}!")]
        public string Password { get; set; }


        [Display(Name = "First Name")]
        [MaxLength(30, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? FirstName { get; set; }


        [Display(Name = "Last Name")]
        [MaxLength(30, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? LastName { get; set; }


        [Display(Name = "Phone Number")]
        [MaxLength(15, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? PhonNumber { get; set; }


        [Display(Name = "Condition")]
        public int IsActive { get; set; }


        public IFormFile? UserAvatar { get; set; }


        [Display(Name = "Wallet")]
        public int? AddWallet { get; set; }

    }
}
