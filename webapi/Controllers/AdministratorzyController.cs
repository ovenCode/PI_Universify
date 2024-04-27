using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json.Nodes;
using webapi.Data;
using webapi.Models;
using webapi.Models.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorzyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        private readonly Expression<Func<Administrator, AdministratorDTO>> AsAdministratorDTO =
            admin => new AdministratorDTO(admin);

        public AdministratorzyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Administratorzy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministratorDTO>>> GetAdministratorzy()
        {
            if (_context.Administratorzy == null)
            {
                return NotFound();
            }
            return await _context.Administratorzy.Select(AsAdministratorDTO).ToListAsync();
        }

        // GET: api/Administratorzy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministratorDTO>> GetAdministrator(long id)
        {
            if (_context.Administratorzy == null)
            {
                return NotFound();
            }
            var administrator = await _context.Administratorzy.Where(admin => admin.IdAdministratora == id).Select(AsAdministratorDTO).SingleAsync();

            if (administrator == null)
            {
                return NotFound();
            }

            return administrator;
        }

        // PUT: api/Administratorzy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrator(long id, AdministratorDTO administrator)
        {
            if (await _context.Administratorzy.FindAsync(id) == null)
            {
                return BadRequest();
            }

            /*            Administrator? admin = await _context.Administratorzy.FindAsync(id);
                        if(admin == null)
                        {
                            return NotFound();
                        }

                        admin.IdUżytkownika = administrator.IdUżytkownika;
                        admin.IdRoli = administrator.IdRoli;*/

            _context.Entry(administrator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/Administratorzy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdministratorDTO>> PostAdministrator(Administrator administrator)
        {
            if (_context.Administratorzy == null)
            {
                return Problem("Entity set 'UniversifyDbContext.Administratorzy'  is null.");
            }

            administrator.IdUżytkownika = _context.Użytkownicy.Count() + 1;
            administrator.IdAdministratora = _context.Administratorzy.Count() + 1;
            administrator.Rola = _context.Role.Where(r => r.IdRoli == administrator.IdRoli).First();
            _context.Administratorzy.Add(administrator);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdministrator), new { id = administrator.IdAdministratora }, administrator);
        }

        // DELETE: api/Administratorzy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrator(long id)
        {
            if (_context.Administratorzy == null)
            {
                return NotFound();
            }
            var administrator = await _context.Administratorzy.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            _context.Administratorzy.Remove(administrator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdministratorExists(long id)
        {
            return (_context.Administratorzy?.Any(e => e.IdAdministratora == id)).GetValueOrDefault();
        }

        /*private static AdministratorDTO ItemToDTO(Administrator admin) => new AdministratorDTO
       {
           IdAdministratora = admin.IdAdministratora,
           IdUżytkownika = admin.IdUżytkownika,
           IdRoli = admin.IdRoli
       };*/
    }
}
