using DataLayer.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Entities.Wallets
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }


        [Display(Name = "Transaction type")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int TypeId { get; set; }


        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int UserId { get; set; }


        [Display(Name = "Abount")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int Amount { get; set; }


        [Display(Name = "Description")]
        [MaxLength(300)]
        public string Description { get; set; }


        [Display(Name = "Is Pay")]
        public bool IsPay { get; set; }


        [Display(Name = "Date Time")]
        public DateTime CreateDate { get; set; }





        #region RELATION

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("TypeId")]
        public WalletType WalletType { get; set; }

        #endregion
    }
}
