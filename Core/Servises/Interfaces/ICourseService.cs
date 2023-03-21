using Core.DTOs;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Servises.Interfaces
{
    public interface ICourseService
    {
        List<ShowCourseForAdminViewModel> GetCourseForAdmin(string CourseNameFilter = "");
        List<SelectListItem> GetGroupForManageCourse();
        User GetTeacher(string teacherName);
    }
}
