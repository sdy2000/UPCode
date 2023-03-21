using Core.DTOs;

namespace Core.Servises.Interfaces
{
    public interface ICourseService
    {
        List<ShowCourseForAdminViewModel> GetCourseForAdmin(string CourseNameFilter = "");
    }
}
