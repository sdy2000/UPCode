using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Courses
{
    public class CourseStatus
    {
        [Key]
        public int StatusId { get; set; }


        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string StatusTitle { get; set; }




        #region RELATION

        public List<Course> Courses { get; set; }

        #endregion
    }
}
