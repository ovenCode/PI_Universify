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
    public class StołówkiController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public StołówkiController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Stołówki
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stołówka>>> GetStołówki()
        {
          if (_context.Stołówki == null)
          {
              return NotFound();
          }
            return await _context.Stołówki
                .Include(s => s.Zamówienia).ThenInclude(z => z.Danie)
                .Include(s => s.Zamówienia).ThenInclude(z => z.Dieta)
                .Include(s => s.Budynek).ToListAsync();
        }

        // GET: api/Stołówki/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stołówka>> GetStołówka(long id)
        {
          if (_context.Stołówki == null)
          {
              return NotFound();
          }
            var stołówka = await _context.Stołówki
                .Include(s => s.Zamówienia).ThenInclude(z => z.Danie)
                .Include(s => s.Zamówienia).ThenInclude(z => z.Dieta)
                .Include(s => s.Budynek).SingleOrDefaultAsync(s => s.IdStołówki == id);

            if (stołówka == null)
            {
                return NotFound();
            }

            return stołówka;
        }

        // PUT: api/Stołówki/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStołówka(long id, Stołówka stołówka)
        {
            if (id != stołówka.IdStołówki)
            {
                return BadRequest();
            }

            _context.Entry(stołówka).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StołówkaExists(id))
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

        // POST: api/Stołówki
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stołówka>> PostStołówka(Stołówka stołówka)
        {
          if (_context.Stołówki == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Stołówki'  is null.");
          }
            _context.Stołówki.Add(stołówka);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StołówkaExists(stołówka.IdStołówki))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStołówka", new { id = stołówka.IdStołówki }, stołówka);
        }

        // DELETE: api/Stołówki/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStołówka(long id)
        {
            if (_context.Stołówki == null)
            {
                return NotFound();
            }
            var stołówka = await _context.Stołówki.FindAsync(id);
            if (stołówka == null)
            {
                return NotFound();
            }

            _context.Stołówki.Remove(stołówka);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StołówkaExists(long id)
        {
            return (_context.Stołówki?.Any(e => e.IdStołówki == id)).GetValueOrDefault();
        }

        private static StołówkaDTO ItemToDTO(Stołówka stołówka) => new StołówkaDTO
        {
            IdStołówki = stołówka.IdStołówki,
            IdBudynku = stołówka.IdBudynku,
            IdZamówienia = stołówka.IdZamówienia,
            IdProduktu = stołówka.IdProduktu,
            InformacjeDodatkowe = stołówka.InformacjeDodatkowe,
            Budynek = stołówka.Budynek,
            Produkty = stołówka.Produkty,
            Zamówienia = stołówka.Zamówienia
        };
    }    
}


