using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Courses
{
    public class CourseLevel
    {
        [Key]
        public int LevelId { get; set; }


        [Display(Name = "Course Level")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(150, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string LevelTitle { get; set; }




        #region RELATION

        public List<Course> Courses { get; set; }

        #endregion
    }
}
