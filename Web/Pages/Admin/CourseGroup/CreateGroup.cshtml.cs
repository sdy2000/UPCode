using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.CourseGroup
{
    [PermissionChecker(22)]
    public class CreateGroupModel : PageModel
    {
        private ICourseService _courseService;
        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Courses.CourseGroup CourseGroup { get; set; }
        public void OnGet(int id)
        {
            CourseGroup = new DataLayer.Entities.Courses.CourseGroup()
            {
                ParentId = id
            };
        }


        public IActionResult OnPost()
        {
            if (CourseGroup.GroupTitle == null)
                return Page();

            if (CourseGroup.ParentId == 0)
            {
                CourseGroup.ParentId = null;
            }
            _courseService.AddGroup(CourseGroup);

            return Redirect("/Admin/CourseGroup");
        }
    }
}
