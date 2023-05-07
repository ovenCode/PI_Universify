using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WydziałyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public WydziałyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Wydziały
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wydział>>> GetWydziały()
        {
          if (_context.Wydziały == null)
          {
              return NotFound();
          }
            return await _context.Wydziały.ToListAsync();
        }

        // GET: api/Wydziały/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wydział>> GetWydział(long id)
        {
          if (_context.Wydziały == null)
          {
              return NotFound();
          }
            var wydział = await _context.Wydziały.FindAsync(id);

            if (wydział == null)
            {
                return NotFound();
            }

            return wydział;
        }

        // PUT: api/Wydziały/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWydział(long id, Wydział wydział)
        {
            if (id != wydział.IdWydziału)
            {
                return BadRequest();
            }

            _context.Entry(wydział).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WydziałExists(id))
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

        // POST: api/Wydziały
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wydział>> PostWydział(Wydział wydział)
        {
          if (_context.Wydziały == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Wydziały'  is null.");
          }
            _context.Wydziały.Add(wydział);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWydział", new { id = wydział.IdWydziału }, wydział);
        }

        // DELETE: api/Wydziały/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWydział(long id)
        {
            if (_context.Wydziały == null)
            {
                return NotFound();
            }
            var wydział = await _context.Wydziały.FindAsync(id);
            if (wydział == null)
            {
                return NotFound();
            }

            _context.Wydziały.Remove(wydział);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WydziałExists(long id)
        {
            return (_context.Wydziały?.Any(e => e.IdWydziału == id)).GetValueOrDefault();
        }
    }
}
