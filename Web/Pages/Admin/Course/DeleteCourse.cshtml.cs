using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Course
{
    [PermissionChecker(17)]
    public class DeleteCourseModel : PageModel
    {
        private ICourseService _courseService;
        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [BindProperty]
        public DataLayer.Entities.Courses.Course Course { get; set; }
        public void OnGet(int id)
        {
            ViewData["Teachers"] = _courseService.GetTeacher(User.Identity.Name);
            Course = _courseService.GetCourseById(id);
        }
    }
}
