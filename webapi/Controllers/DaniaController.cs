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
    public class DaniaController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public DaniaController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Dania
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Danie>>> GetDania()
        {
          if (_context.Dania == null)
          {
              return NotFound();
          }
            return await _context.Dania
                .Include(d => d.Składniki)
                .Include(d => d.Dieta).ThenInclude(di => di.Alergeny)
                .ToListAsync();
        }

        // GET: api/Dania/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Danie>> GetDanie(long id)
        {
          if (_context.Dania == null)
          {
              return NotFound();
          }
            var danie = await _context.Dania
                .Include(d => d.Składniki)
                .Include(d => d.Dieta).ThenInclude(di => di.Alergeny)
                .SingleOrDefaultAsync(d => d.IdDania == id);

            if (danie == null)
            {
                return NotFound();
            }

            return danie;
        }

        // PUT: api/Dania/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanie(long id, Danie danie)
        {
            if (id != danie.IdDania)
            {
                return BadRequest();
            }

            _context.Entry(danie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanieExists(id))
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

        // POST: api/Dania
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Danie>> PostDanie(Danie danie)
        {
          if (_context.Dania == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Dania'  is null.");
          }
            _context.Dania.Add(danie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDanie", new { id = danie.IdDania }, danie);
        }

        // DELETE: api/Dania/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanie(long id)
        {
            if (_context.Dania == null)
            {
                return NotFound();
            }
            var danie = await _context.Dania.FindAsync(id);
            if (danie == null)
            {
                return NotFound();
            }

            _context.Dania.Remove(danie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DanieExists(long id)
        {
            return (_context.Dania?.Any(e => e.IdDania == id)).GetValueOrDefault();
        }
    }
}
