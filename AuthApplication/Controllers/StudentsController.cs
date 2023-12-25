using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthApplication.Model;
using Microsoft.AspNetCore.Authorization;

namespace AuthApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public StudentsController(MyDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }


        [HttpPost,Authorize]
        public async Task<ActionResult<Student>> PostStudent(InputStudent s)
        {
            var student = new Student
            {
                StudentName = s.StudentName,
                Age = s.Age,
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutStudent(int id, InputStudent s)
        {
            var student = _context.Students.Find(id);
            student.StudentName = s.StudentName;
            student.Age=s.Age;
            _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();         

            return Ok(student);
        }

        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("Successfully Removed");
        }

    }
}
