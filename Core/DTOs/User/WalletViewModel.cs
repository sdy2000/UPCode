using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "Abount")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int Amount { get; set; }
    }

    public class WalletViewModel
    {
        public int Amount { get; set; }
        public int Type{ get; set; }
        public bool IsPay { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
