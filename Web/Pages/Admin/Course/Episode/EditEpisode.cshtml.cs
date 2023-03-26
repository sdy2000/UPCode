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
    }
}
