using Core.DTOs;
using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Course
{
    [PermissionChecker(13)]
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public List<ShowCourseForAdminViewModel> CourseList { get; set; }
        public void OnGet(string CourseNameFilter = "")
        {
            CourseList = _courseService.GetCourseForAdmin(CourseNameFilter);
        }
    }
}
