using Core.Security;
using Core.Servises.Interfaces;
using DataLayer.Entities.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Course.Episode
{
    [PermissionChecker(17)]
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public List<CourseEpisode> EpisodesList { get; set; }
        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;
            EpisodesList = _courseService.GetAllCourseEpisode(id);
        }
    }
}
