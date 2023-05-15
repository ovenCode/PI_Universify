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
    public class ProduktyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public ProduktyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Produkty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produkt>>> GetProdukty()
        {
          if (_context.Produkty == null)
          {
              return NotFound();
          }
            return await _context.Produkty.ToListAsync();
        }

        // GET: api/Produkty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produkt>> GetProdukt(long id)
        {
          if (_context.Produkty == null)
          {
              return NotFound();
          }
            var produkt = await _context.Produkty.FindAsync(id);

            if (produkt == null)
            {
                return NotFound();
            }

            return produkt;
        }

        // PUT: api/Produkty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdukt(long id, Produkt produkt)
        {
            if (id != produkt.IdProduktu)
            {
                return BadRequest();
            }

            _context.Entry(produkt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduktExists(id))
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

        // POST: api/Produkty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produkt>> PostProdukt(Produkt produkt)
        {
          if (_context.Produkty == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Produkty'  is null.");
          }
            _context.Produkty.Add(produkt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdukt", new { id = produkt.IdProduktu }, produkt);
        }

        // DELETE: api/Produkty/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdukt(long id)
        {
            if (_context.Produkty == null)
            {
                return NotFound();
            }
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }

            _context.Produkty.Remove(produkt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduktExists(long id)
        {
            return (_context.Produkty?.Any(e => e.IdProduktu == id)).GetValueOrDefault();
        }
    }
}
