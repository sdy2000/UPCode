using Core.DTOs;
using DataLayer.Entities.Courses;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Servises.Interfaces
{
    public interface ICourseService
    {
        string CourseImagePath(string folderName, string imgName);
        string SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg");
        string SaveOrUpdateFile(IFormFile demoCourse, string CourseDemoName = null);
        bool SaveChange();


        int AddCourse(Course course, IFormFile imgCourse, IFormFile demoCourse);

        List<ShowCourseForAdminViewModel> GetCourseForAdmin(string CourseNameFilter = "");
        List<SelectListItem> GetGroupForManageCourse();
        User GetTeacher(string teacherName);
    }
}
