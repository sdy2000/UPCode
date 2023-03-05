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
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? Email { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? LastName { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? PhonNumber { get; set; }



        [Display(Name = "جنسیت")]
        public int? GenderId { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string UserAvatar { get; set; }


        public IFormFile? UserImage { get; set; }
    }

    public class EditProfileInfoViewModel
    {
        public bool IsEditProfile { get; set; }
        public bool IsSendActiveEmail { get; set; }
        public bool IsSuccess { get; set; }
    }
}
