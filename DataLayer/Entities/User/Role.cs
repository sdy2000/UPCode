using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.User
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }


        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string RoleTitle { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        [Display(Name = "حذف شده")]
        public bool IsDelete { get; set; }





        #region RELATIONS



        #endregion
    }
}
