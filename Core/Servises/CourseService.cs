using Core.DTOs;
using Core.Servises.Interfaces;
using DataLayer.Context;

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
    }
}
