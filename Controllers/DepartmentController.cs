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
    public class DepartmentController : ControllerBase
    {
        private readonly StudentContext db;
        public DepartmentController(StudentContext _db)
        {
            db = _db;
        }
        [HttpGet]
        [Route("/api/Dept/GetAll/")]
        public IActionResult GetAll()
        {
            var depts = db.Departments.Include(e => e.students).ToList();

            if (depts == null || depts.Count == 0)
            {
                return NotFound(new { msg = "No departments found" });
            }

            List<DeptWithStdntName> deptDTOs = new List<DeptWithStdntName>();

            foreach (var dept in depts)
            {
                DeptWithStdntName deptDTO = new DeptWithStdntName();
                deptDTO.Department_Number = dept.Id;
                deptDTO.Department_Name = dept.Name;
                deptDTO.Department_Manger = dept.MgrName;
                deptDTO.Department_Location = dept.Location;

                if (dept.students != null)
                {
                    foreach (var e in dept.students)
                    {
                        deptDTO.Student_Name.Add(e.Name);
                    }
                }

                deptDTOs.Add(deptDTO);
            }

            return Ok(new { msg = "Departments found", Depts = deptDTOs });
        }

        [HttpGet]
        [Route("/api/Dept/Details/{id:int}")]
        public IActionResult GetById(int id)
        {
            
            var dept = db.Departments.Include(e => e.students).FirstOrDefault(d => d.Id == id);

            if (dept is null)
            { return NotFound(new { msg = $"Dept with Id = {id} Not Found" }); }
            DeptWithStdntName deptDTO = new DeptWithStdntName();
            deptDTO.Department_Number = dept.Id;
            deptDTO.Department_Name = dept.Name;
            deptDTO.Department_Manger = dept.MgrName;
            deptDTO.Department_Location = dept.Location;
            foreach (var e in dept.students)
            {
                deptDTO.Student_Name.Add(e.Name);
            }

            return Ok(new { msg = $"Dept with Id = {id}  Found", Dept = deptDTO });
        }
        [HttpGet]
        [Route("/api/Dept/Details/{Name:alpha}")] //related path .. /details/ is abslute path
        public IActionResult GetByName(string Name)
        {
            var dept = db.Departments.Include(e => e.students).FirstOrDefault(d => d.Name == Name);


            if (dept is null)
            { return NotFound(new { msg = $"Dept with Name = {Name}, Not Found" }); }
            DeptWithStdntName deptDTO = new DeptWithStdntName();
            deptDTO.Department_Number = dept.Id;
            deptDTO.Department_Name = dept.Name;
            deptDTO.Department_Manger = dept.MgrName;
            deptDTO.Department_Location = dept.Location;
            foreach (var e in dept.students)
            {
                deptDTO.Student_Name.Add(e.Name);
            }

            return Ok(new { msg = $"Dept with Name = {Name}, Is Found", Dept = deptDTO });

        }
        [HttpPost]
        [Route("/api/Dept/Add")]
        public IActionResult Add(Department dept)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(dept);
                db.SaveChanges();

                // Use the department's Id to create the URL for the newly created resource
                string url = Url.Link("GetById", new { id = dept.Id });

                // Return a Created response with the URL of the newly created resource
                return Created(url, new { msg = $"Department with Id = {dept.Id} created successfully" });
            }

            // Return a BadRequest response if the model state is not valid
            return BadRequest(new { msg = "Invalid data provided for department creation" });
        }

        [HttpPut("/api/Dept/Edit2/{id}")]
        public IActionResult EditV01(int id, Department dept)
        {
            var oldDept = db.Departments.Find(id);

            if (ModelState.IsValid)
            {

                oldDept.Name = dept.Name;
                oldDept.Location = dept.Location;
                oldDept.MgrName = dept.MgrName;
                db.SaveChanges();
                return StatusCode(204, "updated Successfuly");
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/api/Dept/Delete/")]


        public IActionResult Delete(int id)
        {
            var oldDept = db.Departments.Find(id);
            if (oldDept is null)

                return NotFound();
            db.Departments.Remove(oldDept);
            db.SaveChanges();

            return StatusCode(204, "Record Deleted");
        }
    }
}
