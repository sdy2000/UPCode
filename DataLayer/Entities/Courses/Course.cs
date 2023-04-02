using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Users;

namespace DataLayer.Entities.Courses
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }


        [Display(Name = "Group")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int GroupId { get; set; }


        public int? SubGroupId { get; set; }


        [Required]
        public int TeacherId { get; set; }


        [Display(Name = "Course Level")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int LevelId { get; set; }


        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int StatusId { get; set; }


        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string CourseTitle { get; set; }


        [Display(Name = "Course Description")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string CourseDescription { get; set; }


        [Display(Name = "Corse Price")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public int CoursePrice { get; set; }


        [Display(Name = "Tags")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(150, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string Tags { get; set; }


        [Display(Name = "Course Image")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string CourseImageName { get; set; }


        [Display(Name = "Course Demo")]
        public string? CourseDemoFileName { get; set; }



        [Display(Name = "Is Deleted Course")]
        public bool IsDelete { get; set; } = false;


        [Display(Name = "Course Craete Date")]
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; set; }







        #region RELATION


        [ForeignKey("TeacherId")]
        public User User { get; set; }

        [ForeignKey("GroupId")]
        public CourseGroup Group { get; set; }

        [ForeignKey("SubGroupId")]
        public CourseGroup SubGroup { get; set; }

        [ForeignKey("LevelId")]
        public CourseLevel CourseLevel { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus CourseStatus { get; set; }

        public List<CourseEpisode> CourseEpisodes { get; set; }

        public List<CourseComment> CourseComments { get; set; }

        #endregion
    }
}
