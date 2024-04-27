using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KierunkiStudiówController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public KierunkiStudiówController(UniversifyDbContext context)
        {
            _context = context;
        }
        // GET: api/KierunkiStudiów
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KierunekStudiówDTO>>> GetKierunkiStudiów()
        {
            if (_context.KierunkiStudiów == null)
            {
                return NotFound();
            }

            return await _context.KierunkiStudiów.Include(k => k.Studenci).Select(ks => new KierunekStudiówDTO(ks)).ToListAsync();
        }

        // GET api/KierunkiStudiów/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KierunekStudiówDTO>> GetKierunekStudiów(int id)
        {
            if (_context.KierunkiStudiów == null)
            {
                return NotFound();
            }
            
            var kierunek = await _context.KierunkiStudiów.Where(ks => ks.IdKierunkuStudiów == id).Select(ks => new KierunekStudiówDTO(ks)).SingleOrDefaultAsync();

            if (kierunek == null)
            {
                return NotFound();
            }

            return kierunek;
        }

        // POST api/KierunkiStudiów
        [HttpPost]
        public async Task<ActionResult<KierunekStudiów>> PostKierunekStudiów(KierunekStudiów value)
        {
            if (_context.KierunkiStudiów == null)
            {
                return Problem("Entity set 'UniversifyDBContext.KierunkiStudiów' is null.");
            }

            _context.KierunkiStudiów.Add(value);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKierunekStudiów", new { id = value.IdKierunkuStudiów }, value);
        }

        // PUT api/KierunkiStudiów/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKierunekStudiów(int id, KierunekStudiów value)
        {
            if (id != value.IdKierunkuStudiów)
            {
                return BadRequest();
            }

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KierunekStudiówExists(id))
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

        // DELETE api/KierunkiStudiów/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKierunekStudiów(long id)
        {
            if (_context.KierunkiStudiów == null)
            {
                return NotFound();
            }

            var kierunek = await _context.KierunkiStudiów.FindAsync(id);
            if(kierunek == null)
            {
                return NotFound();
            }

            _context.KierunkiStudiów.Remove(kierunek);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KierunekStudiówExists(long id)
        {
            return (_context.KierunkiStudiów.Any(e => e.IdKierunkuStudiów == id));
        }
    }
}
