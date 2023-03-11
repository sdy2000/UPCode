using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Entities.Wallets
{
    public class WalletType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }


        [Required]
        [MaxLength(100)]
        public string TypeTitle { get; set; }




        #region RELATIONS

        public List<Wallet> Wallets { get; set; }

        #endregion
    }
}
