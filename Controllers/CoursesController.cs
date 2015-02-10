using AngularJsMvcDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AngularJsMvcDemo.Controllers
{
    public class CoursesController : ApiController, IDisposable
    {
        private DemoDBEntities _context;

        public CoursesController()
        {
            _context = new DemoDBEntities();
        }

        // GET api/<controller>
        public IEnumerable<Models.Course> Get()
        {
            return (from c in _context.Courses
                    select new Models.Course
                    {
                        CourseName = c.CourseTitle,
                        CourseId = c.CourseId,
                        CourseCode = c.CourseCode
                    }).AsEnumerable();
        }

        // GET api/<controller>/5
        public Models.Course Get(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                return new Models.Course
                {
                    CourseName = course.CourseTitle,
                    CourseId = course.CourseId,
                    CourseCode = course.CourseCode
                };
            }
            return null;
        }

        // POST api/<controller>
        public void Post([FromBody]Models.Course entity)
        {
            var item = new Data.Course
            {
                CourseId = entity.CourseId,
                CourseCode = entity.CourseCode,
                CourseTitle = entity.CourseName
            };
            _context.Courses.Add(item);
            _context.SaveChanges();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Models.Course entity)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                course.CourseTitle = entity.CourseName;
                course.CourseCode = entity.CourseCode;
                _context.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }

        void IDisposable.Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}