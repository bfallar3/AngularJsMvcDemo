using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJsMvcDemo.Models;
using AngularJsMvcDemo.Data;

namespace AngularJsMvcDemo.Controllers
{
    public class StudentsController : ApiController, IDisposable
    {
        private Data.DemoDBEntities _context;

        public StudentsController()
        {
            _context = new Data.DemoDBEntities();
        }

        // GET api/<controller>
        public IEnumerable<Models.Student> Get()
        {
            var query = from s in _context.Students
                        select new Models.Student
                        {
                            StudentId = s.StudentId,
                            FirstName = s.Firstname,
                            LastName = s.Lastname,
                            Email = s.Email,
                            Course = s.Course
                        };
            return query.AsEnumerable();
        }

        // GET api/<controller>/5
        public Models.Student Get(int id)
        {
            var student = _context.Students.Find(id);
            return new Models.Student
            {
                StudentId = student.StudentId,
                FirstName = student.Firstname,
                LastName = student.Lastname,
                Email = student.Email,
                Course = student.Course
            };
        }

        // POST api/<controller>
        public void Post([FromBody]Models.Student value)
        {
            var entity = new Data.Student
            {
                StudentId = value.StudentId,
                Firstname = value.FirstName,
                Lastname = value.LastName,
                Course = value.Course,
                Email = value.Email
            };
            _context.Students.Add(entity);
            _context.SaveChanges();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Models.Student value)
        {
            var updated = _context.Students.Find(id);
            if (updated != null)
            {
                updated.Firstname = value.FirstName;
                updated.Lastname = value.LastName;
                updated.Course = value.Course;
                updated.Email = value.Email;
                _context.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
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