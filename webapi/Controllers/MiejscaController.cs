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
    public class MiejscaController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public MiejscaController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Miejsca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Miejsce>>> GetMiejsca()
        {
          if (_context.Miejsca == null)
          {
              return NotFound();
          }
            return await _context.Miejsca.ToListAsync();
        }

        // GET: api/Miejsca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Miejsce>> GetMiejsce(long id)
        {
          if (_context.Miejsca == null)
          {
              return NotFound();
          }
            var miejsce = await _context.Miejsca.FindAsync(id);

            if (miejsce == null)
            {
                return NotFound();
            }

            return miejsce;
        }

        // PUT: api/Miejsca/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMiejsce(long id, Miejsce miejsce)
        {
            if (id != miejsce.IdMiejsca)
            {
                return BadRequest();
            }

            _context.Entry(miejsce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MiejsceExists(id))
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

        // POST: api/Miejsca
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Miejsce>> PostMiejsce(Miejsce miejsce)
        {
          if (_context.Miejsca == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Miejsca'  is null.");
          }
            _context.Miejsca.Add(miejsce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMiejsce", new { id = miejsce.IdMiejsca }, miejsce);
        }

        // DELETE: api/Miejsca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMiejsce(long id)
        {
            if (_context.Miejsca == null)
            {
                return NotFound();
            }
            var miejsce = await _context.Miejsca.FindAsync(id);
            if (miejsce == null)
            {
                return NotFound();
            }

            _context.Miejsca.Remove(miejsce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MiejsceExists(long id)
        {
            return (_context.Miejsca?.Any(e => e.IdMiejsca == id)).GetValueOrDefault();
        }
    }
}
