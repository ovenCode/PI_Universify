using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZamówieniaController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public ZamówieniaController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Zamówienia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zamówienie>>> GetZamówienia()
        {
          if (_context.Zamówienia == null)
          {
              return NotFound();
          }
            return await _context.Zamówienia
                .Include(z => z.Dieta).ThenInclude(d => d.Alergeny)
                .Include(z => z.Danie).ThenInclude(d => d.Składniki)
                .Include(z => z.Stołówka)
                .ToListAsync();
        }

        // GET: api/Zamówienia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zamówienie>> GetZamówienie(long id)
        {
          if (_context.Zamówienia == null)
          {
              return NotFound();
          }
            var zamówienie = await _context.Zamówienia
                .Include(z => z.Dieta).ThenInclude(d => d.Alergeny)
                .Include(z => z.Danie).ThenInclude(d => d.Składniki)
                .Include(z => z.Stołówka)
                .SingleOrDefaultAsync(s => s.IdZamówienia == id);

            if (zamówienie == null)
            {
                return NotFound();
            }

            return zamówienie;
        }

        // PUT: api/Zamówienia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZamówienie(long id, Zamówienie zamówienie)
        {
            if (id != zamówienie.IdZamówienia)
            {
                return BadRequest();
            }

            _context.Entry(zamówienie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZamówienieExists(id))
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

        // POST: api/Zamówienia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zamówienie>> PostZamówienie(Zamówienie zamówienie)
        {
          if (_context.Zamówienia == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Zamówienia'  is null.");
          }
            zamówienie.Danie = _context.Dania.Where(d => d.IdDania == zamówienie.IdDania).First();
            zamówienie.Dieta = _context.Diety.Where(d => d.IdDiety == zamówienie.IdDiety).First();
            zamówienie.Stołówka = _context.Stołówki.Where(s => s.IdStołówki == zamówienie.IdStołówki).First();
            zamówienie.Użytkownik = _context.Użytkownicy.Where(u => u.IdUżytkownika == zamówienie.IdUżytkownika).First();
            _context.Zamówienia.Add(zamówienie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZamówienie", new { id = zamówienie.IdZamówienia }, zamówienie);
        }

        // DELETE: api/Zamówienia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZamówienie(long id)
        {
            if (_context.Zamówienia == null)
            {
                return NotFound();
            }
            var zamówienie = await _context.Zamówienia.FindAsync(id);
            if (zamówienie == null)
            {
                return NotFound();
            }

            _context.Zamówienia.Remove(zamówienie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZamówienieExists(long id)
        {
            return (_context.Zamówienia?.Any(e => e.IdZamówienia == id)).GetValueOrDefault();
        }
    }
}
