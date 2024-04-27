using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.Models.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NauczycieleController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public NauczycieleController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Nauczyciele
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NauczycielDTO>>> GetNauczyciele()
        {
            if (_context.Nauczyciele == null)
            {
                return NotFound();
            }


            return await _context.Nauczyciele
            .Include(n => n.Przedmioty).ThenInclude(p => p.Przedmiot).ThenInclude(np => np.Studenci).ThenInclude(s => s.Student)
            .Include(n => n.Grupy).ThenInclude(g => g.Grupa)
            .Include(n => n.Wydział)
            .Select(n => new NauczycielDTO(n))
            .ToListAsync();
        }

        // GET: api/Nauczyciele/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NauczycielDTO>> GetNauczyciel(long id)
        {
            if (_context.Nauczyciele == null)
            {
                return NotFound();
            }
            var nauczyciel = await _context.Nauczyciele
            .Where(n => n.IdNauczyciela == id)
            .Include(n => n.Przedmioty).ThenInclude(p => p.Przedmiot).ThenInclude(np => np.Studenci).ThenInclude(s => s.Student)
            .Include(n => n.Grupy).ThenInclude(g => g.Grupa)
            .Include(n => n.Wydział)
            .Select(n => new NauczycielDTO(n))
            .SingleOrDefaultAsync();

            if (nauczyciel == null)
            {
                return NotFound();
            }

            return nauczyciel;
        }

        // PUT: api/Nauczyciele/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNauczyciel(long id, Nauczyciel nauczyciel)
        {
            if (id != nauczyciel.IdNauczyciela)
            {
                return BadRequest();
            }

            _context.Entry(nauczyciel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NauczycielExists(id))
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

        // POST: api/Nauczyciele
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nauczyciel>> PostNauczyciel(Nauczyciel nauczyciel)
        {
            if (_context.Nauczyciele == null)
            {
                return Problem("Entity set 'UniversifyDbContext.Nauczyciele'  is null.");
            }

            nauczyciel.IdUżytkownika = _context.Użytkownicy.Count() + 1;
            nauczyciel.IdNauczyciela = _context.Nauczyciele.Count() + 1;
            nauczyciel.Wydział = _context.Wydziały.Where(w => w.IdWydziału == nauczyciel.IdWydziału).First();
            nauczyciel.Specjalizacja = _context.Specjalizacje.Where(s => s.IdSpecjalizacji == nauczyciel.IdSpecjalizacji).First();
            _context.Nauczyciele.Add(nauczyciel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNauczyciel", new { id = nauczyciel.IdUżytkownika }, nauczyciel);
        }

        // DELETE: api/Nauczyciele/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNauczyciel(long id)
        {
            if (_context.Nauczyciele == null)
            {
                return NotFound();
            }
            var nauczyciel = await _context.Nauczyciele.FindAsync(id);
            if (nauczyciel == null)
            {
                return NotFound();
            }

            _context.Nauczyciele.Remove(nauczyciel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NauczycielExists(long id)
        {
            return (_context.Nauczyciele?.Any(e => e.IdNauczyciela == id)).GetValueOrDefault();
        }
    }
}
