using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Course
{
    public class CreateCourseModel : PageModel
    {
        private ICourseService _courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Courses.Course Course { get; set; }
        public void OnGet()
        {
            List<SelectListItem> group = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Seleted",Value="" }
            }; group.AddRange(_courseService.GetGroupForManageCourse());
            ViewData["Groups"] = new SelectList(group, "Value", "Text");

            List<SelectListItem> subGroup = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Selected",Value="" }
            };
            ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text");

            ViewData["Teachers"] = _courseService.GetTeacher(User.Identity.Name);


        }
    }
}
