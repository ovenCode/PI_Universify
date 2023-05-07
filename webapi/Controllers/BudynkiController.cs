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
    public class BudynkiController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public BudynkiController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Budynki
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Budynek>>> GetBudynki()
        {
          if (_context.Budynki == null)
          {
              return NotFound();
          }
            return await _context.Budynki.ToListAsync();
        }

        // GET: api/Budynki/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Budynek>> GetBudynek(long id)
        {
          if (_context.Budynki == null)
          {
              return NotFound();
          }
            var budynek = await _context.Budynki.FindAsync(id);

            if (budynek == null)
            {
                return NotFound();
            }

            return budynek;
        }

        // PUT: api/Budynki/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudynek(long id, Budynek budynek)
        {
            if (id != budynek.IdBudynku)
            {
                return BadRequest();
            }

            _context.Entry(budynek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudynekExists(id))
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

        // POST: api/Budynki
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Budynek>> PostBudynek(Budynek budynek)
        {
          if (_context.Budynki == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Budynki'  is null.");
          }
            _context.Budynki.Add(budynek);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudynek", new { id = budynek.IdBudynku }, budynek);
        }

        // DELETE: api/Budynki/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudynek(long id)
        {
            if (_context.Budynki == null)
            {
                return NotFound();
            }
            var budynek = await _context.Budynki.FindAsync(id);
            if (budynek == null)
            {
                return NotFound();
            }

            _context.Budynki.Remove(budynek);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BudynekExists(long id)
        {
            return (_context.Budynki?.Any(e => e.IdBudynku == id)).GetValueOrDefault();
        }
    }
}
