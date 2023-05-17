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
    public class TypyMiejscController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public TypyMiejscController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/TypyMiejsc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypMiejsca>>> GetTypyMiejsc()
        {
          if (_context.TypyMiejsc == null)
          {
              return NotFound();
          }
            return await _context.TypyMiejsc.ToListAsync();
        }

        // GET: api/TypyMiejsc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypMiejsca>> GetTypMiejsca(long id)
        {
          if (_context.TypyMiejsc == null)
          {
              return NotFound();
          }
            var typMiejsca = await _context.TypyMiejsc.FindAsync(id);

            if (typMiejsca == null)
            {
                return NotFound();
            }

            return typMiejsca;
        }

        // PUT: api/TypyMiejsc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypMiejsca(long id, TypMiejsca typMiejsca)
        {
            if (id != typMiejsca.IdTypu)
            {
                return BadRequest();
            }

            _context.Entry(typMiejsca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypMiejscaExists(id))
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

        // POST: api/TypyMiejsc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypMiejsca>> PostTypMiejsca(TypMiejsca typMiejsca)
        {
          if (_context.TypyMiejsc == null)
          {
              return Problem("Entity set 'UniversifyDbContext.TypyMiejsc'  is null.");
          }
            _context.TypyMiejsc.Add(typMiejsca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypMiejsca", new { id = typMiejsca.IdTypu }, typMiejsca);
        }

        // DELETE: api/TypyMiejsc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypMiejsca(long id)
        {
            if (_context.TypyMiejsc == null)
            {
                return NotFound();
            }
            var typMiejsca = await _context.TypyMiejsc.FindAsync(id);
            if (typMiejsca == null)
            {
                return NotFound();
            }

            _context.TypyMiejsc.Remove(typMiejsca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypMiejscaExists(long id)
        {
            return (_context.TypyMiejsc?.Any(e => e.IdTypu == id)).GetValueOrDefault();
        }
    }
}
