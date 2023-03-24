using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Course
{
    [PermissionChecker(15)]
    public class EditCourseModel : PageModel
    {
        private ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Courses.Course Course { get; set; }
        public void OnGet(int id)
        {
            Course = _courseService.GetCourseById(id);


            #region INPUT VALIUES


            List<SelectListItem> group = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Selected",Value="" }
            }; group.AddRange(_courseService.GetGroupForManageCourse());
            ViewData["Groups"] = new SelectList(group, "Value", "Text", Course.GroupId);

            List<SelectListItem> subGroup = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Selected",Value="" }
            }; subGroup.AddRange(_courseService.GetSubGroupForManageCourse(Course.GroupId));
            ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text", Course.SubGroupId ?? 0);

            ViewData["Teachers"] = _courseService.GetTeacher(User.Identity.Name);

            #endregion

        }
    }
}
