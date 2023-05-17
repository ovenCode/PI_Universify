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
    public class SkładnikiController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public SkładnikiController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Składniki
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Składnik>>> GetSkładniki()
        {
          if (_context.Składniki == null)
          {
              return NotFound();
          }
            return await _context.Składniki.ToListAsync();
        }

        // GET: api/Składniki/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Składnik>> GetSkładnik(long id)
        {
          if (_context.Składniki == null)
          {
              return NotFound();
          }
            var składnik = await _context.Składniki.FindAsync(id);

            if (składnik == null)
            {
                return NotFound();
            }

            return składnik;
        }

        // PUT: api/Składniki/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkładnik(long id, Składnik składnik)
        {
            if (id != składnik.IdSkładnika)
            {
                return BadRequest();
            }

            _context.Entry(składnik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkładnikExists(id))
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

        // POST: api/Składniki
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Składnik>> PostSkładnik(Składnik składnik)
        {
          if (_context.Składniki == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Składniki'  is null.");
          }
            _context.Składniki.Add(składnik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkładnik", new { id = składnik.IdSkładnika }, składnik);
        }

        // DELETE: api/Składniki/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkładnik(long id)
        {
            if (_context.Składniki == null)
            {
                return NotFound();
            }
            var składnik = await _context.Składniki.FindAsync(id);
            if (składnik == null)
            {
                return NotFound();
            }

            _context.Składniki.Remove(składnik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkładnikExists(long id)
        {
            return (_context.Składniki?.Any(e => e.IdSkładnika == id)).GetValueOrDefault();
        }
    }
}
