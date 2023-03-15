using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Datalayer.Entities.Wallets;

namespace DataLayer.Entities.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "Gender")]
        public int? GenderId { get; set; }


        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string UserName { get; set; }


        [Display(Name = "Name")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? FirstName { get; set; }


        [Display(Name = "Last Name")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? LastName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        [MaxLength(200, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string? PhonNumber { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string Password { get; set; }


        [Display(Name = "Avatar")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string UserAvatar { get; set; }


        [Display(Name = "Active Code")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string ActiveCode { get; set; }


        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        [Display(Name = "Reagister Date")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;


        [Display(Name = "Is Deleted")]
        public bool IsDelete { get; set; }





        #region RELATIONS

        [ForeignKey("GenderId")]
        public UserGender UserGender { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Wallet> Wallets { get; set; }

        #endregion

    }
}
