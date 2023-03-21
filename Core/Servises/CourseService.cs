using Core.DTOs;
using Core.Servises.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Servises
{
    public class CourseService: ICourseService
    {
        private UPCodeContext _context;

        public CourseService(UPCodeContext context)
        {
            _context = context;
        }


        public List<ShowCourseForAdminViewModel> GetCourseForAdmin(string CourseNameFilter = "")
        {
            if (!string.IsNullOrEmpty(CourseNameFilter))
            {

                return _context.Courses
                    .Where(c => c.CourseTitle.Contains(CourseNameFilter))
                    .Select(c => new ShowCourseForAdminViewModel()
                    {
                        CourseId = c.CourseId,
                        CourseTitle = c.CourseTitle,
                        ImageName = c.CourseImageName,
                        EpisodeCount = c.CourseEpisodes.Count()
                    })
                    .ToList();
            }

            return _context.Courses
                .Select(c => new ShowCourseForAdminViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    EpisodeCount = c.CourseEpisodes.Count()
                })
                .ToList();
        }

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                })
                .ToList();
        }

        public User GetTeacher(string teacherName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == teacherName);
        }
    }
}
