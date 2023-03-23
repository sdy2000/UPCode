using Core.Security;
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

        public IActionResult OnPost(IFormFile demoUp, IFormFile ImgeUp)
        {
            #region VALIDATEION

            if (string.IsNullOrEmpty(Course.CourseTitle) || string.IsNullOrEmpty(Course.CourseDescription) ||
                string.IsNullOrEmpty(Course.Tags) || Course.GroupId == null || Course.LevelId == null || Course.StatusId == null)
            {
                List<SelectListItem> group = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="Selected",Value="" }
                }; group.AddRange(_courseService.GetGroupForManageCourse());
                ViewData["Groups"] = new SelectList(group, "Value", "Text");

                List<SelectListItem> subGroup = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="Selected",Value="" }
                }; ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text");

                ViewData["Teachers"] = _courseService.GetTeacher(User.Identity.Name);

                ViewData["IsSuccess"] = false;
                return Page();
            }
            if (!ImgeUp.IsImage())
            {
                List<SelectListItem> group = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="Selected",Value="" }
                }; group.AddRange(_courseService.GetGroupForManageCourse());
                ViewData["Groups"] = new SelectList(group, "Value", "Text");

                List<SelectListItem> subGroup = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="Selected",Value="" }
                }; ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text");

                ViewData["Teachers"] = _courseService.GetTeacher(User.Identity.Name);

                ModelState.AddModelError("Course.CourseImageName", "Image UnValid");
                ViewData["IsSuccess"] = false;
                return Page();
            }

            #endregion


            _courseService.AddCourseFromAdminPanel(Course, ImgeUp, demoUp);

            return Redirect("Index");
        }
    }
}
