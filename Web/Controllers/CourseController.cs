using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> SelectedGroups = null)
        {
            ViewBag.SelectedGroups = SelectedGroups;
            ViewBag.Groups = _courseService.GetAllGroup();
            ViewBag.pageId = pageId;
            return View(_courseService.GetCourseForView(pageId, filter, getType, orderByType, startPrice, endPrice, SelectedGroups, 9));
        }


        [Route("ShowCourse/{id}")]
        public IActionResult ShowCourse(int id, int episodeId = 0)
        {
            var course = _courseService.GetCourseForShow(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
    }
}
