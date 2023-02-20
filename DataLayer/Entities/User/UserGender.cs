using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.User
{
    public class UserGender
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenderId { get; set; }


        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string GenderTitle { get; set; }



        #region RELATION

        public List<User> Users { get; set; }

        #endregion

    }
}
