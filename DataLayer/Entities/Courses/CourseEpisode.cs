using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Courses
{

    public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }


        [Required]
        public int CourseId { get; set; }


        [Display(Name = "Episode Title")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string EpisodeTitle { get; set; }


        [Display(Name = "Episode File")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public string EpisodFileName { get; set; }


        [Display(Name = "Is Free")]
        public bool IsFree { get; set; }


        [Display(Name = "Is Deleted")]
        public bool IsDelete { get; set; } = false;


        [Display(Name = "Create Date")]
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        [Display(Name = "Episode Time")]
        [Required(ErrorMessage = "Please enter {0}.")]
        public TimeSpan EpisodeTime { get; set; }





        #region RELATION

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        #endregion
    }
}
