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
    public class SpecjalizacjeController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public SpecjalizacjeController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Specjalizacje
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specjalizacja>>> GetSpecjalizacje()
        {
          if (_context.Specjalizacje == null)
          {
              return NotFound();
          }
            return await _context.Specjalizacje.ToListAsync();
        }

        // GET: api/Specjalizacje/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specjalizacja>> GetSpecjalizacja(long id)
        {
          if (_context.Specjalizacje == null)
          {
              return NotFound();
          }
            var specjalizacja = await _context.Specjalizacje.FindAsync(id);

            if (specjalizacja == null)
            {
                return NotFound();
            }

            return specjalizacja;
        }

        // PUT: api/Specjalizacje/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecjalizacja(long id, Specjalizacja specjalizacja)
        {
            if (id != specjalizacja.IdSpecjalizacji)
            {
                return BadRequest();
            }

            _context.Entry(specjalizacja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecjalizacjaExists(id))
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

        // POST: api/Specjalizacje
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specjalizacja>> PostSpecjalizacja(Specjalizacja specjalizacja)
        {
          if (_context.Specjalizacje == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Specjalizacje'  is null.");
          }
            _context.Specjalizacje.Add(specjalizacja);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecjalizacja", new { id = specjalizacja.IdSpecjalizacji }, specjalizacja);
        }

        // DELETE: api/Specjalizacje/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecjalizacja(long id)
        {
            if (_context.Specjalizacje == null)
            {
                return NotFound();
            }
            var specjalizacja = await _context.Specjalizacje.FindAsync(id);
            if (specjalizacja == null)
            {
                return NotFound();
            }

            _context.Specjalizacje.Remove(specjalizacja);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecjalizacjaExists(long id)
        {
            return (_context.Specjalizacje?.Any(e => e.IdSpecjalizacji == id)).GetValueOrDefault();
        }
    }
}
