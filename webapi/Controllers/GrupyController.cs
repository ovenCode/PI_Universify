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
    public class GrupyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public GrupyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Grupy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grupa>>> GetGrupy()
        {
          if (_context.Grupy == null)
          {
              return NotFound();
          }
            return await _context.Grupy.ToListAsync();
        }

        // GET: api/Grupy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grupa>> GetGrupa(long id)
        {
          if (_context.Grupy == null)
          {
              return NotFound();
          }
            var grupa = await _context.Grupy.FindAsync(id);

            if (grupa == null)
            {
                return NotFound();
            }

            return grupa;
        }

        // PUT: api/Grupy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrupa(long id, Grupa grupa)
        {
            if (id != grupa.IdGrupy)
            {
                return BadRequest();
            }

            _context.Entry(grupa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupaExists(id))
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

        // POST: api/Grupy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grupa>> PostGrupa(Grupa grupa)
        {
          if (_context.Grupy == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Grupy'  is null.");
          }
            _context.Grupy.Add(grupa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrupa", new { id = grupa.IdGrupy }, grupa);
        }

        // DELETE: api/Grupy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupa(long id)
        {
            if (_context.Grupy == null)
            {
                return NotFound();
            }
            var grupa = await _context.Grupy.FindAsync(id);
            if (grupa == null)
            {
                return NotFound();
            }

            _context.Grupy.Remove(grupa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GrupaExists(long id)
        {
            return (_context.Grupy?.Any(e => e.IdGrupy == id)).GetValueOrDefault();
        }
    }
}
