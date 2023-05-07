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
    public class AlergenyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public AlergenyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Alergeny
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alergen>>> GetAlergeny()
        {
          if (_context.Alergeny == null)
          {
              return NotFound();
          }
            return await _context.Alergeny.ToListAsync();
        }

        // GET: api/Alergeny/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alergen>> GetAlergen(long id)
        {
          if (_context.Alergeny == null)
          {
              return NotFound();
          }
            var alergen = await _context.Alergeny.FindAsync(id);

            if (alergen == null)
            {
                return NotFound();
            }

            return alergen;
        }

        // PUT: api/Alergeny/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlergen(long id, Alergen alergen)
        {
            if (id != alergen.IdAlergenu)
            {
                return BadRequest();
            }

            _context.Entry(alergen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlergenExists(id))
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

        // POST: api/Alergeny
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alergen>> PostAlergen(Alergen alergen)
        {
          if (_context.Alergeny == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Alergeny'  is null.");
          }
            _context.Alergeny.Add(alergen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlergen", new { id = alergen.IdAlergenu }, alergen);
        }

        // DELETE: api/Alergeny/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlergen(long id)
        {
            if (_context.Alergeny == null)
            {
                return NotFound();
            }
            var alergen = await _context.Alergeny.FindAsync(id);
            if (alergen == null)
            {
                return NotFound();
            }

            _context.Alergeny.Remove(alergen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlergenExists(long id)
        {
            return (_context.Alergeny?.Any(e => e.IdAlergenu == id)).GetValueOrDefault();
        }
    }
}
