using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.User
{
    public class UserGender
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenderId { get; set; }


        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string GenderTitle { get; set; }



        #region RELATION

        public List<User> Users { get; set; }

        #endregion

    }
}
