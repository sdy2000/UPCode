using Core.Servises.Interfaces;
using DataLayer.Entities.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Course.Episode
{
    public class EditEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public EditEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            CourseEpisode = _courseService.GetEpisodeById(id);
        }

        public IActionResult OnPost(IFormFile fileUp)
        {
            if (string.IsNullOrEmpty(CourseEpisode.EpisodeTitle))
            {
                ViewData["IsSuccess"] = false;
                return Page();
            }

            _courseService.UpdateEpisodeFormAdmin(CourseEpisode, fileUp);

            return Redirect("/Admin/Course/Episode/Index/" + CourseEpisode.CourseId);
        }
    }
}
