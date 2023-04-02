using DataLayer.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Courses
{
    public class CourseComment
    {
        [Key]
        public int CommetnId { get; set; }


        public int CourseId { get; set; }


        public int UserId { get; set; }


        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(900, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string Comment { get; set; }


        public DateTime CreateDate { get; set; }


        public bool IsDelete { get; set; }


        public bool IsAdminRead { get; set; }





        #region Relation

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion
    }
}
