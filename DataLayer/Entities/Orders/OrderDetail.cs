using DataLayer.Entities.Courses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Orders
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }


        [Required]
        public int OrderId { get; set; }


        [Required]
        public int CourseId { get; set; }


        [Required]
        public int Count { get; set; }


        [Required]
        public int Price { get; set; }




        #region RALATION    

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        #endregion
    }
}
