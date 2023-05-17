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
    public class DietyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public DietyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Diety
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dieta>>> GetDiety()
        {
          if (_context.Diety == null)
          {
              return NotFound();
          }
            return await _context.Diety.ToListAsync();
        }

        // GET: api/Diety/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dieta>> GetDieta(long id)
        {
          if (_context.Diety == null)
          {
              return NotFound();
          }
            var dieta = await _context.Diety.FindAsync(id);

            if (dieta == null)
            {
                return NotFound();
            }

            return dieta;
        }

        // PUT: api/Diety/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDieta(long id, Dieta dieta)
        {
            if (id != dieta.IdDiety)
            {
                return BadRequest();
            }

            _context.Entry(dieta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DietaExists(id))
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

        // POST: api/Diety
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dieta>> PostDieta(Dieta dieta)
        {
          if (_context.Diety == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Diety'  is null.");
          }
            _context.Diety.Add(dieta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDieta", new { id = dieta.IdDiety }, dieta);
        }

        // DELETE: api/Diety/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDieta(long id)
        {
            if (_context.Diety == null)
            {
                return NotFound();
            }
            var dieta = await _context.Diety.FindAsync(id);
            if (dieta == null)
            {
                return NotFound();
            }

            _context.Diety.Remove(dieta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DietaExists(long id)
        {
            return (_context.Diety?.Any(e => e.IdDiety == id)).GetValueOrDefault();
        }
    }
}
