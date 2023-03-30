using Core.Security;
using Core.Servises.Interfaces;
using DataLayer.Entities.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Course.Episode
{
    [PermissionChecker(20)]
    public class DeleteEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public DeleteEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            CourseEpisode = _courseService.GetEpisodeById(id);
        }

        public IActionResult OnPost()
        {
            _courseService.DeleteEpisode(CourseEpisode.EpisodeId);
            return Redirect("/Admin/Course/Episode/" + CourseEpisode.CourseId);
        }
    }
}
