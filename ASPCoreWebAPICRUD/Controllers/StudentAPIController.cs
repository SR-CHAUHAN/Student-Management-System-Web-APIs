using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext context;
        public StudentAPIController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        // GET: api/StudentAPI/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(student);
        }

        // POST: api/StudentAPI
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            context.Students.Add(student);
            await context.SaveChangesAsync();

            return Ok(student);
        }

        // PUT: api/StudentAPI/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            
            if (id != student.Id)
            {
                return BadRequest();
            }
            context.Entry(student).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = context.Students.Find(id);
            if(student == null) 
            {
                return NotFound();
            }
            context.Students.Remove(student);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}