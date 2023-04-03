using DataLayer.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Orders
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }


        [Required]
        public int UserId { get; set; }


        [Required]
        public int OrderSum { get; set; }


        public bool IsFinaly { get; set; }


        [Required]
        public DateTime CreateDate { get; set; }




        #region RALATION

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

        #endregion

    }
}
