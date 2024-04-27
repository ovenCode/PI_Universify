using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

            System.Console.WriteLine(_context.Model.ToDebugString());

            return await _context.Role.Include(r => r.Uprawnienia).ToListAsync();
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rola>> GetRola(long id)
        {
            if (_context.Role == null)
            {
                return NotFound();
            }
            var rola = await _context.Role.Include(r => r.Uprawnienia).SingleAsync(r => r.IdRoli == id);

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
            Rola change = await _context.Role.Include(r => r.Uprawnienia).SingleAsync(r => r.IdRoli == id);

            if (change == null)
            {
                return BadRequest();
            }

            change.Nazwa = rola.Nazwa;
            change.Uprawnienia = rola.Uprawnienia.Select((u, index) => new Uprawnienie
            {
                IdUprawnienia = u.IdUprawnienia,
                Nazwa = u.Nazwa,
                Role = u.Role
            }).ToList();

            _context.Entry(change).State = EntityState.Modified;


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
            if (RolaExists(rola.IdRoli, rola.Nazwa))
            {
                return BadRequest("Nie można dodać roli o istniejącej nazwie. Zmień nazwę i spróbuj ponownie.");
            }
            List<Uprawnienie> uprawnienia = await _context.Uprawnienia.Where(e => rola.Uprawnienia.Contains(e)).ToListAsync();
            System.Console.WriteLine(JsonSerializer.Serialize(rola));
            Rola newRole = new Rola
            {
                Nazwa = rola.Nazwa
            };
            foreach (var u in uprawnienia)
            {
                newRole.Uprawnienia.Add(u);
            }
            _context.Role.Add(newRole);
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
            else if (new string[] { "Administrator systemu", "Nauczyciel", "Student" }.Contains(rola.Nazwa))
            {
                return BadRequest("Usunięcie tej roli jest zabronione");
            }

            _context.Role.Remove(rola);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolaExists(long id, string name = "")
        {
            if (name.Length > 0)
            {
                return (_context.Role?.Any(e => e.Nazwa == name)).GetValueOrDefault();
            }
            return (_context.Role?.Any(e => e.IdRoli == id)).GetValueOrDefault();
        }
    }
}
