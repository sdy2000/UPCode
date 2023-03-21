using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Courses
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }


        [Display(Name = "Croup Title")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string GroupTitle { get; set; }


        [Display(Name = "Is Delete")]
        public bool IsDelete { get; set; }


        [Display(Name = "Parent")]
        public int? ParentId { get; set; }





        #region RELATION

        [ForeignKey("ParentId")]
        public List<CourseGroup> CourseGroups { get; set; }

        [InverseProperty("Group")]
        public List<Course> Groups { get; set; }

        [InverseProperty("SubGroup")]
        public List<Course> SubGroups { get; set; }


        #endregion
    }
}
