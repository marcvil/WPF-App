using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A4.DAL;
using A4.Lib.Models;
using Common.Lib.Infrastructure;

namespace ASPNET_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AcademyDbContext _context;

        public StudentsController(AcademyDbContext context)
        {
            _context = context;

          
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentDb()
        {
           return await _context.StudentDb.ToListAsync();
        }

       

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            var student = await _context.StudentDb.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<SaveValidation<Student>> PutStudent(Guid id, Student student)
        {
            return await Task.Run(() =>
            {
                return student.Save();
            });
        }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<SaveValidation<Student>> PostStudent(Student student)
        {
            return await Task.Run(() =>
            {
                return student.Save();
            });
                 
        }

        /* public async Task<ActionResult<Student>> PostStudent(Student student)
         {
             _context.StudentDb.Add(student);
             await _context.SaveChangesAsync();

             return CreatedAtAction(nameof (GetStudent), new { id = student.Id }, student);
         }
         */

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(Guid id)
        {
            var student = await _context.StudentDb.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.StudentDb.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(Guid id)
        {
            return _context.StudentDb.Any(e => e.Id == id);
        }
    }
}
