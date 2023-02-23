using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد!")]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند!")]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string RePassword { get; set; }


        [Display(Name = "قوانین و مقررات")]
        [Required(ErrorMessage = "لطفا {0} را تایید نمایید.")]
        public bool Rules { get; set; }
    }
}
