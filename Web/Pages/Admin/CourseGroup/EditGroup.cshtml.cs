using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.CourseGroup
{
    [PermissionChecker(23)]
    public class EditGroupModel : PageModel
    {
        private ICourseService _courseService;
        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Courses.CourseGroup CourseGroup { get; set; }
        public void OnGet(int id)
        {
            CourseGroup = _courseService.GetGroupById(id);
        }

        public IActionResult OnPost()
        {
            if (CourseGroup.GroupTitle == null)
                return Page();

            if (CourseGroup.ParentId == 0)
            {
                CourseGroup.ParentId = null;
            }
            _courseService.UpdateGroup(CourseGroup);

            return Redirect("/Admin/CourseGroup");
        }
    }
}
