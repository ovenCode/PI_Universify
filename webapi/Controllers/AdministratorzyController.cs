using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorzyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

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
            return await _context.Administratorzy.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/Administratorzy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministratorDTO>> GetAdministrator(long id)
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

            return ItemToDTO(administrator);
        }

        // PUT: api/Administratorzy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrator(long id, AdministratorDTO administrator)
        {
            if (id != administrator.IdAdministratora)
            {
                return BadRequest();
            }

            Administrator? admin = await _context.Administratorzy.FindAsync(id);
            if(admin == null)
            {
                return NotFound();
            }

            admin.IdUżytkownika = administrator.IdUżytkownika;
            admin.IdRoli = administrator.IdRoli;

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
        public async Task<ActionResult<AdministratorDTO>> PostAdministrator(AdministratorDTO administrator)
        {
          if (_context.Administratorzy == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Administratorzy'  is null.");
          }

            Administrator admin = new Administrator
            {
                IdAdministratora = administrator.IdAdministratora,
                IdUżytkownika = administrator.IdUżytkownika,
                IdRoli = administrator.IdRoli
            };
            _context.Administratorzy.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdministrator), new { id = admin.IdAdministratora }, ItemToDTO(admin));
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

        private static AdministratorDTO ItemToDTO(Administrator admin) => new AdministratorDTO
       {
           IdAdministratora = admin.IdAdministratora,
           IdUżytkownika = admin.IdUżytkownika,
           IdRoli = admin.IdRoli
       };
    }
}
