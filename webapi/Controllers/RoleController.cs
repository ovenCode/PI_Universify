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
    public class RoleController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public RoleController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rola>>> GetRole()
        {
          if (_context.Role == null)
          {
              return NotFound();
          }
            return await _context.Role.ToListAsync();
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rola>> GetRola(long id)
        {
          if (_context.Role == null)
          {
              return NotFound();
          }
            var rola = await _context.Role.FindAsync(id);

            if (rola == null)
            {
                return NotFound();
            }

            return rola;
        }

        // PUT: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRola(long id, Rola rola)
        {
            if (id != rola.IdRoli)
            {
                return BadRequest();
            }

            _context.Entry(rola).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolaExists(id))
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

        // POST: api/Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rola>> PostRola(Rola rola)
        {
          if (_context.Role == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Role'  is null.");
          }
            _context.Role.Add(rola);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRola", new { id = rola.IdRoli }, rola);
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRola(long id)
        {
            if (_context.Role == null)
            {
                return NotFound();
            }
            var rola = await _context.Role.FindAsync(id);
            if (rola == null)
            {
                return NotFound();
            }

            _context.Role.Remove(rola);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolaExists(long id)
        {
            return (_context.Role?.Any(e => e.IdRoli == id)).GetValueOrDefault();
        }
    }
}
