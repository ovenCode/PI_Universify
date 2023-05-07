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
    public class KategorieController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public KategorieController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Kategorie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kategoria>>> GetKategorie()
        {
          if (_context.Kategorie == null)
          {
              return NotFound();
          }
            return await _context.Kategorie.ToListAsync();
        }

        // GET: api/Kategorie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kategoria>> GetKategoria(long id)
        {
          if (_context.Kategorie == null)
          {
              return NotFound();
          }
            var kategoria = await _context.Kategorie.FindAsync(id);

            if (kategoria == null)
            {
                return NotFound();
            }

            return kategoria;
        }

        // PUT: api/Kategorie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKategoria(long id, Kategoria kategoria)
        {
            if (id != kategoria.IdKategorii)
            {
                return BadRequest();
            }

            _context.Entry(kategoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategoriaExists(id))
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

        // POST: api/Kategorie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kategoria>> PostKategoria(Kategoria kategoria)
        {
          if (_context.Kategorie == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Kategorie'  is null.");
          }
            _context.Kategorie.Add(kategoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKategoria", new { id = kategoria.IdKategorii }, kategoria);
        }

        // DELETE: api/Kategorie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKategoria(long id)
        {
            if (_context.Kategorie == null)
            {
                return NotFound();
            }
            var kategoria = await _context.Kategorie.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }

            _context.Kategorie.Remove(kategoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KategoriaExists(long id)
        {
            return (_context.Kategorie?.Any(e => e.IdKategorii == id)).GetValueOrDefault();
        }
    }
}
