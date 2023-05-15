using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudenciController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public StudenciController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Studenci
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudenci()
        {
          if (_context.Studenci == null)
          {
              return NotFound();
          }
            return await _context.Studenci.ToListAsync();
        }

        // GET: api/Studenci/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
          if (_context.Studenci == null)
          {
              return NotFound();
          }
            var student = await _context.Studenci.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Studenci/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, Student student)
        {
            if (id != student.IdStudenta)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Studenci
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
          if (_context.Studenci == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Studenci'  is null.");
          }
            _context.Studenci.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.IdStudenta }, student);
        }

        // DELETE: api/Studenci/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            if (_context.Studenci == null)
            {
                return NotFound();
            }
            var student = await _context.Studenci.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Studenci.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(long id)
        {
            return (_context.Studenci?.Any(e => e.IdStudenta == id)).GetValueOrDefault();
        }
    }
}
