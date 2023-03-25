using Core.Security;
using Core.Servises.Interfaces;
using DataLayer.Entities.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Course.Episode
{
    [PermissionChecker(18)]
    public class CreateEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;
        }

        public IActionResult OnPost(IFormFile fileUp)
        {
            if (string.IsNullOrEmpty(CourseEpisode.EpisodeTitle) || fileUp == null)
            {
                ViewData["IsSuccess"] = false;
                ViewData["CourseId"] = CourseEpisode.CourseId;
                return Page();
            }

            _courseService.AddEpisodeFormAdmin(CourseEpisode, fileUp);

            return Redirect("/Admin/Course/Episode/Index/" + CourseEpisode.CourseId);
        }
    }
}
