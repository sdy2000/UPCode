using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.CourseGroup
{
    [PermissionChecker(24)]
    public class DeleteGroupModel : PageModel
    {
        private ICourseService _courseService;
        public DeleteGroupModel(ICourseService courseService)
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
            _courseService.DeleteGroup(CourseGroup.GroupId);

            return Redirect("/Admin/CourseGroup");
        }
    }
}
