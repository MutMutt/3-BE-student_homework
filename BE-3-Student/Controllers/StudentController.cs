using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BE_3_Student.Data;
using BE_3_Student.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BE_3_Student.Models;
using DTO;

namespace BE_3_Student.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly Context _context;

        public StudentController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudent()
        {
            var student = from students in _context.students
                       join students_description in _context.students_description on students.id equals students_description.id
                       select new StudentDTO
                       {
                           Grade = students.grade,
                           Students_Id = students_description.students_id,
                           Age = students_description.age,
                           First_Name = students_description.first_name,
                           Last_Name = students_description.last_name,
                           Address = students_description.address,
                           Country = students_description.country,
                       };

            return await student.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDTO> GetStudent_byId(int id)
        {
            var student = from students in _context.students
                          join students_description in _context.students_description on students.id equals students_description.id
                          select new StudentDTO
                          {
                              Grade = students.grade,
                              Students_Id = students_description.students_id,
                              Age = students_description.age,
                              First_Name = students_description.first_name,
                              Last_Name = students_description.last_name,
                              Address = students_description.address,
                              Country = students_description.country,
                          };

            var student_by_id = student.ToList().Find(x => x.Students_Id == id);

            if (student_by_id == null)
            {
                return NotFound();
            }
            return student_by_id;
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Add_Students(AddStudent studentDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Students()
            {
                id = studentDTO.Students_Id ,
                grade = studentDTO.Grade
            };
            await _context.students.AddAsync(student);
            await _context.SaveChangesAsync();

            var student_description = new Students_description()
            {
                students_id = studentDTO.Students_Id,
                age = studentDTO.Age,
                first_name = studentDTO.First_Name,
                last_name = studentDTO.Last_Name,
                address = studentDTO.Address,
                country = studentDTO.Country
            };
            await _context.AddAsync(student_description);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooks", new { id = student.id}, studentDTO);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Students>> Delete_Student(int id)
        {
            var student = _context.students.Find(id);
            var student_description = _context.students_description.SingleOrDefault(x => x.students_id == id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(student);
                _context.Remove(student_description);
                await _context.SaveChangesAsync();
                return student;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update_Student(int id, StudentDTO student)
        {
            if (id != student.Students_Id || !StudentExists(id))
            {
                return BadRequest();
            }
            else
            {
                var students = _context.students.SingleOrDefault(x => x.id == id);
                var students_description = _context.students_description.SingleOrDefault(x => x.students_id == id);

                students.id = student.Students_Id;
                students.grade = student.Grade;
                students_description.age = student.Age;
                students_description.country = student.Country;
                students_description.first_name = student.First_Name;
                students_description.last_name = student.Last_Name;
                students_description.address = student.Address;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        private bool StudentExists(int id)
        {
            return _context.students.Any(x => x.id == id);
        }
    }
}
