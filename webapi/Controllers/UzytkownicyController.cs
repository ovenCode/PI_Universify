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
    public class UzytkownicyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public UzytkownicyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Uzytkownicy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Użytkownik>>> GetUżytkownicy()
        {
          if (_context.Użytkownicy == null)
          {
              return NotFound();
          }
            return await _context.Użytkownicy.ToListAsync();
        }

        // GET: api/Uzytkownicy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Użytkownik>> GetUżytkownik(long id)
        {
          if (_context.Użytkownicy == null)
          {
              return NotFound();
          }
            var użytkownik = await _context.Użytkownicy.FindAsync(id);

            if (użytkownik == null)
            {
                return NotFound();
            }

            return użytkownik;
        }

        // PUT: api/Uzytkownicy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUżytkownik(long id, Użytkownik użytkownik)
        {
            if (id != użytkownik.IdUżytkownika)
            {
                return BadRequest();
            }

            _context.Entry(użytkownik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UżytkownikExists(id))
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

        // POST: api/Uzytkownicy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Użytkownik>> PostUżytkownik(Użytkownik użytkownik)
        {
          if (_context.Użytkownicy == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Użytkownicy'  is null.");
          }
            _context.Użytkownicy.Add(użytkownik);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUżytkownik), new { id = użytkownik.IdUżytkownika }, użytkownik);
        }

        // DELETE: api/Uzytkownicy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUżytkownik(long id)
        {
            if (_context.Użytkownicy == null)
            {
                return NotFound();
            }
            var użytkownik = await _context.Użytkownicy.FindAsync(id);
            if (użytkownik == null)
            {
                return NotFound();
            }

            _context.Użytkownicy.Remove(użytkownik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UżytkownikExists(long id)
        {
            return (_context.Użytkownicy?.Any(e => e.IdUżytkownika == id)).GetValueOrDefault();
        }
    }
}
