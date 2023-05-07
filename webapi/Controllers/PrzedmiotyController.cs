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
    public class PrzedmiotyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public PrzedmiotyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Przedmioty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Przedmiot>>> GetPrzedmioty()
        {
          if (_context.Przedmioty == null)
          {
              return NotFound();
          }
            return await _context.Przedmioty.ToListAsync();
        }

        // GET: api/Przedmioty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Przedmiot>> GetPrzedmiot(long id)
        {
          if (_context.Przedmioty == null)
          {
              return NotFound();
          }
            var przedmiot = await _context.Przedmioty.FindAsync(id);

            if (przedmiot == null)
            {
                return NotFound();
            }

            return przedmiot;
        }

        // PUT: api/Przedmioty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrzedmiot(long id, Przedmiot przedmiot)
        {
            if (id != przedmiot.IdPrzedmiotu)
            {
                return BadRequest();
            }

            _context.Entry(przedmiot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrzedmiotExists(id))
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

        // POST: api/Przedmioty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Przedmiot>> PostPrzedmiot(Przedmiot przedmiot)
        {
          if (_context.Przedmioty == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Przedmioty'  is null.");
          }
            _context.Przedmioty.Add(przedmiot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrzedmiot", new { id = przedmiot.IdPrzedmiotu }, przedmiot);
        }

        // DELETE: api/Przedmioty/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrzedmiot(long id)
        {
            if (_context.Przedmioty == null)
            {
                return NotFound();
            }
            var przedmiot = await _context.Przedmioty.FindAsync(id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            _context.Przedmioty.Remove(przedmiot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrzedmiotExists(long id)
        {
            return (_context.Przedmioty?.Any(e => e.IdPrzedmiotu == id)).GetValueOrDefault();
        }
    }
}
