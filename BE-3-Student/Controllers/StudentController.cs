using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BE_3_Student.Data;
using BE_3_Student.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetBooks()
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
        public ActionResult<StudentDTO> GetBooks_byId(int id)
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
    }
}
