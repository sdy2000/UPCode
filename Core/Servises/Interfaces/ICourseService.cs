using Core.DTOs;
using DataLayer.Entities.Courses;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Core.Servises.Interfaces
{
    public interface ICourseService
    {
        bool SaveChange();



        #region COURSE

        bool AddCourse(Course course);
        bool UpdateCourse(Course course);
        void DeleteCourse(int CourseId);
        Course GetCourseById(int courseId);
        User GetTeacher(string teacherName); 
        string CourseImagePath(string folderName, string imgName);
        string SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg");
        string SaveOrUpdateFile(IFormFile demoCourse, string CourseDemoName = null, string filePath = "wwwroot/CourseImage/demoes");

        int AddCourseFromAdminPanel(Course course, IFormFile imgCourse, IFormFile demoCourse);
        int UpdateCourseFromAdmin(Course course, IFormFile imgCourse, IFormFile demoCourse);
        List<ShowCourseForAdminViewModel> GetCourseForAdmin(string CourseNameFilter = "");
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int parentId);
        #endregion

        #region COURSE EPISODE

        List<CourseEpisode> GetAllCourseEpisode(int courseId);
        CourseEpisode GetEpisodeById(int episodeId);
        bool AddEpisode(CourseEpisode episode); 
        bool UpdateEpisode(CourseEpisode episode);
        int DeleteEpisode(int episodeId);

        int AddEpisodeFormAdmin(CourseEpisode episode, IFormFile fileUp);
        int UpdateEpisodeFormAdmin(CourseEpisode episode, IFormFile fileUp);

        #endregion
        #region GROUP

        List<CourseGroup> GetAllGroup();
        bool AddGroup(CourseGroup group);
        bool UpdateGroup(CourseGroup group);
        void DeleteGroup(int groupId);
        CourseGroup GetGroupById(int groupId);

        #endregion
    }
}
