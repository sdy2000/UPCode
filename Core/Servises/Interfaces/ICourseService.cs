using Core.DTOs;
using DataLayer.Entities.Courses;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Servises.Interfaces
{
    public interface ICourseService
    {
        bool SaveChange();



        #region COURSE

        bool AddCourse(Course course);
        User GetTeacher(string teacherName); 
        string CourseImagePath(string folderName, string imgName);
        string SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg");
        string SaveOrUpdateFile(IFormFile demoCourse, string CourseDemoName = null);

        int AddCourseFromAdminPanel(Course course, IFormFile imgCourse, IFormFile demoCourse);
        List<ShowCourseForAdminViewModel> GetCourseForAdmin(string CourseNameFilter = "");
        List<SelectListItem> GetGroupForManageCourse();
        #endregion


        #region GROUP

        List<CourseGroup> GetAllGroup();
        bool AddGroup(CourseGroup group);

        #endregion
    }
}
