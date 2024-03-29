﻿using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Servises.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Courses;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Core.Servises
{
    public class CourseService : ICourseService
    {
        private UPCodeContext _context;

        public CourseService(UPCodeContext context)
        {
            _context = context;
        }


        // // // // // // // // // Course Admin
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





        // // // // // // // // // Course Episode Admin
        public List<CourseEpisode> GetAllCourseEpisode(int courseId)
        {
            return _context.CourseEpisodes
                .Where(ce => ce.CourseId == courseId)
                .ToList();
        }

        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public bool AddEpisode(CourseEpisode episode)
        {
            try
            {
                _context.CourseEpisodes.Add(episode);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEpisode(CourseEpisode episode)
        {
            try
            {
                _context.CourseEpisodes.Update(episode);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int DeleteEpisode(int episodeId)
        {
            var episode = _context.CourseEpisodes.Find(episodeId);

            episode.IsDelete = true;

            if (UpdateEpisode(episode))
                SaveChange();

            return episode.EpisodeId;
        }


        public int AddEpisodeFormAdmin(CourseEpisode episode, IFormFile fileUp)
        {
            episode.EpisodFileName = SaveOrUpdateFile(fileUp, null, "wwwroot/Course/CourseFile");

            if (AddEpisode(episode))
                SaveChange();

            return episode.EpisodeId;
        }
        public int UpdateEpisodeFormAdmin(CourseEpisode episode, IFormFile fileUp)
        {
            episode.EpisodFileName = SaveOrUpdateFile(fileUp, episode.EpisodFileName, "wwwroot/Course/CourseFile");

            if (UpdateEpisode(episode))
                SaveChange();

            return episode.EpisodeId;
        }


        // // // // // // // // // Group Admin
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


        // // // // // // // // // Course 
        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourseForView(int pageId = 1, string filter = "", string getType = "all",
           string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> SelectedGroups = null, int take = 0)
        {
            IQueryable<Course> result = _context.Courses;

            if (take == 0)
                take = 8;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }
            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }
            }

            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updatedate":
                    {
                        result = result.OrderByDescending(c => c.UpdateDate);
                        break;
                    }
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }
            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < endPrice);
            }

            if (SelectedGroups != null && SelectedGroups.Any())
            {
                foreach (int groupId in SelectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroupId == groupId);
                }
            }


            int skip = (pageId - 1) * take;

            int pageCount = result
                .Include(c => c.CourseEpisodes)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    CourseTitle = c.CourseTitle,

                }).Count() / take;


            var query = result
                .Include(c => c.CourseEpisodes)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    CourseTitle = c.CourseTitle,
                    CourseEpisodes = c.CourseEpisodes
                })
                .Skip(skip).Take(take)
                .ToList();


            return Tuple.Create(query, pageCount);

        }

        public Course GetCourseForShow(int CourseId)
        {
            return _context.Courses
                .Include(c => c.CourseEpisodes)
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel)
                .Include(c => c.User)
                .Include(c => c.UserCourses)
                .FirstOrDefault(c => c.CourseId == CourseId);

        }

        public List<ShowCourseListItemViewModel> GetLastestCourse()
        {
            return _context.Courses
                .OrderByDescending(d => d.CreateDate)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    CourseTitle = c.CourseTitle,
                    CourseEpisodes = c.CourseEpisodes
                })
                .Take(8)
                .ToList();
        }

        public List<ShowCourseListItemViewModel> GetPopularCourse()
        {
            return _context.Courses
                .OrderByDescending(d => d.OrderDetails.Count)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    CourseTitle = c.CourseTitle,
                    CourseEpisodes = c.CourseEpisodes
                })
                .Take(8)
                .ToList();
        }
    }
}
