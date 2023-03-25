using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Servises.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Courses;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Servises
{
    public class CourseService : ICourseService
    {
        private UPCodeContext _context;

        public CourseService(UPCodeContext context)
        {
            _context = context;
        }


        // // // // // // // // // Course
        public bool AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCourse(Course course)
        {
            try
            {
                _context.Courses.Update(course);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteCourse(int CourseId)
        {
            Course course = _context.Courses.Find(CourseId);
            course.IsDelete = true;

            if (UpdateCourse(course))
                SaveChange();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public User GetTeacher(string teacherName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == teacherName);
        }

        public string CourseImagePath(string folderName, string imgName)
        {
            string path = Path.Combine(
                 Directory.GetCurrentDirectory(),
                 "wwwroot/Course/" + folderName,
                 imgName);

            return path;
        }

        public string SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg")
        {


            if (img != null && img.IsImage() && !FileValidator.CheckIfExiclFile(img))
            {

                string normalPath = CourseImagePath("NormalSize", imgName);
                string thumbPath = CourseImagePath("ThumbSize", imgName);
                string iconPath = CourseImagePath("IconSize", imgName);

                if (imgName != "No-Photo.jpg")
                {
                    if (File.Exists(normalPath))
                        File.Delete(normalPath);

                    if (File.Exists(thumbPath))
                        File.Delete(thumbPath);

                    if (File.Exists(iconPath))
                        File.Delete(iconPath);
                }

                imgName = new string
                (Path.GetFileNameWithoutExtension(img.FileName).Take(10).ToArray()).Replace(' ', '-') + "-" +
                NameGenerator.GeneratorUniqCode() + "-" +
                DateTime.Now.ToString("yyyymmssfff") + Path.GetExtension(img.FileName);

                normalPath = CourseImagePath("NormalSize", imgName);
                thumbPath = CourseImagePath("ThumbSize", imgName);
                iconPath = CourseImagePath("IconSize", imgName);

                using (var stream = new FileStream(normalPath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }


                #region RESIZE IMAGE TO THUMB

                ImageConvertor imgResizeThumb = new ImageConvertor();

                imgResizeThumb.Image_resize(normalPath, thumbPath, 184);

                #endregion

                #region RESIZE IMAGE TO ICON

                ImageConvertor imgResize = new ImageConvertor();

                imgResize.Image_resize(normalPath, iconPath, 64);

                #endregion


                return imgName;
            }
            else if (imgName != "No-Photo.jpg")
            {
                return imgName;
            }
            else
            {
                return "No-Photo.jpg";
            }
        }

        public string SaveOrUpdateFile(IFormFile demoCourse, string CourseDemoName = null, string filePath = "wwwroot/Course/demoes")
        {
            if (demoCourse != null)
            {
                string demoPath = "";
                if (CourseDemoName != null)
                {
                    demoPath = Path.Combine(Directory.GetCurrentDirectory(), filePath, CourseDemoName);
                    if (File.Exists(demoPath))
                        File.Delete(demoPath);
                }
                CourseDemoName = NameGenerator.GeneratorUniqCode() + Path.GetExtension(demoCourse.FileName);
                demoPath = Path.Combine(Directory.GetCurrentDirectory(), filePath, CourseDemoName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demoCourse.CopyTo(stream);
                }

                return CourseDemoName;
            }
            else if (CourseDemoName != null)
            {
                return CourseDemoName;
            }
            else
            {
                return null;
            }
        }

        public bool SaveChange()
        {
            try
            {
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }





        public int AddCourseFromAdminPanel(Course course, IFormFile imgCourse, IFormFile demoCourse)
        {
            course.CourseImageName = SaveOrUpDateImg(imgCourse);
            course.CourseDemoFileName = SaveOrUpdateFile(demoCourse);
            course.CoursePrice = (course.CoursePrice == null) ? 0 : course.CoursePrice;


            AddCourse(course);
            SaveChange();

            return course.CourseId;
        }

        public int UpdateCourseFromAdmin(Course course, IFormFile imgCourse, IFormFile demoCourse)
        {
            course.CourseImageName = SaveOrUpDateImg(imgCourse, course.CourseImageName);
            course.CourseDemoFileName = SaveOrUpdateFile(demoCourse, course.CourseDemoFileName);
            course.CoursePrice = (course.CoursePrice == null) ? 0 : course.CoursePrice;
            course.UpdateDate = DateTime.Now;

            if (UpdateCourse(course))
                SaveChange();

            return course.CourseId;
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

        public List<SelectListItem> GetSubGroupForManageCourse(int parentId)
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == parentId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                })
                .ToList();
        }





        // // // // // // // // // Course Episode
        public List<CourseEpisode> GetAllCourseEpisode(int courseId)
        {
            return _context.CourseEpisodes
                .Where(ce => ce.CourseId == courseId)
                .ToList();
        }

        public bool AddEpisode(CourseEpisode episode)
        {
            try
            {
                _context.Courses.Add(episode);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public int AddEpisodeFormAdmin(CourseEpisode episode, IFormFile fileUp)
        {
            episode.EpisodFileName = SaveOrUpdateFile(fileUp, null, "wwwroot/Course/CourseFile");

            if (AddEpisode(episode))
                SaveChange();

            return episode.EpisodeId;
        }



        // // // // // // // // // Group
        public List<CourseGroup> GetAllGroup()
        {
            return _context.CourseGroups.ToList();
        }

        public bool AddGroup(CourseGroup group)
        {
            _context.CourseGroups.Add(group);
            return SaveChange();
        }

        public bool UpdateGroup(CourseGroup group)
        {
            _context.CourseGroups.Update(group);
            return SaveChange();
        }

        public void DeleteGroup(int groupId)
        {
            var group = GetGroupById(groupId);
            group.IsDelete = true;

            UpdateGroup(group);
        }
        public CourseGroup GetGroupById(int groupId)
        {
            return _context.CourseGroups.Find(groupId);
        }
    }
}
