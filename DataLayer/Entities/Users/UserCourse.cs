using DataLayer.Entities.Courses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Users
{
    public class UserCourse
    {
        [Key]
        public int UC_Id { get; set; }


        [Required]
        public int UserId { get; set; }


        [Required]
        public int CourseId { get; set; }





        #region Relation

        [ForeignKey("CourseId")]
        public Course Cource { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion
    }
}
