using BaniSeuifD01.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API_DAY01.Entity;
using WEB_API_DAY01.Models;

namespace WEB_API_DAY01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext db;

        public StudentController(StudentContext _db)
        {
            db = _db;
        }
        [HttpGet]
        [Route("/api/Student/GetAll/")]
        public IActionResult GetAllStudents()
        {
            var students = db.students.Include(s => s.Department).ToList();

            if (students == null || students.Count == 0)
            {
                return NotFound(new { msg = "No students found" });
            }

            List<StdntWithDept> studentDTOs = new List<StdntWithDept>();

            foreach (var student in students)
            {
                StdntWithDept studentDTO = new StdntWithDept
                {
                    Student_Age = student.Age,
                    Student_Name = student.Name,
                    Student_Email = student.Email,
                    Student_Department = student.Department.Name 
                };

                studentDTOs.Add(studentDTO);
            }

            return Ok(new { msg = "Students found", Students = studentDTOs });
        }

        [HttpGet]
        [Route("/api/Student/Details/{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            var student = db.students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);

            if (student is null)
            {
                return NotFound(new { msg = $"Student with id = {id} Not Found" });
            }

            StdntWithDept studentDTO = new StdntWithDept
            {
                Student_Age = student.Age,
                Student_Name = student.Name,
                Student_Email = student.Email,
                Student_Department = student.Department?.Name 
            };

            return Ok(new { msg = $"Student with id = {id} Found", Student = studentDTO });
        }

        [HttpGet]
        [Route("/api/Student/Details/{Name:alpha}")]
        public IActionResult GetStudentByName(string Name)
        {
            var student = db.students.Include(s => s.Department).FirstOrDefault(s => s.Name == Name);

            if (student is null)
            {
                return NotFound(new { msg = $"Student with Name = {Name} Not Found" });
            }

            StdntWithDept studentDTO = new StdntWithDept
            {
                Student_Age = student.Age,
                Student_Name = student.Name,
                Student_Email = student.Email,
                Student_Department = student.Department?.Name 
            };

            return Ok(new { msg = $"Student with Name = {Name} Found", Student = studentDTO });
        }

        [HttpPost]
        public IActionResult Add(Student stdnt)
        {

            if (ModelState.IsValid)
            {
                db.students.Add(stdnt);
                db.SaveChanges();
                //return Ok(stdnt);
                string url = Url.Link("GetOne","");
                return Created(url, "");
                //return Created($"http://localhost:5202/api/student/ {stdnt.Id}", stdnt);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/api/Edit/")]

        //public IActionResult Edit(Student stdnt)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.students.Update(stdnt);
        //        db.SaveChanges();
        //        return StatusCode(204, "updated Successfuly");
        //    }
        //    return BadRequest();
        //}
        [HttpPut("/api/Edit2/{id}")]
        public IActionResult EditV01(int id, Student stdnt)
        {
            var oldStdnt = db.students.Find(id);

            if (ModelState.IsValid)
            {
                oldStdnt.Name = stdnt.Name;
                oldStdnt.Age = stdnt.Age;
                oldStdnt.Email = stdnt.Email;
                db.SaveChanges();
                return StatusCode(204, "updated Successfuly");
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("/api/Delete/")]


        public IActionResult Delete(int id)
        {
            var oldStdnt = db.students.Find(id);
            if (oldStdnt is null)

                return NotFound();
            db.students.Remove(oldStdnt);
            db.SaveChanges();

            return StatusCode(204, "Record Deleted");
        }



    }
}
