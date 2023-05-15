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
    public class GrupyStudenckieController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public GrupyStudenckieController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/GrupyStudenckie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupaStudencka>>> GetGrupyStudenckie()
        {
          if (_context.GrupyStudenckie == null)
          {
              return NotFound();
          }
            return await _context.GrupyStudenckie.ToListAsync();
        }

        // GET: api/GrupyStudenckie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupaStudencka>> GetGrupaStudencka(long id)
        {
          if (_context.GrupyStudenckie == null)
          {
              return NotFound();
          }
            var grupaStudencka = await _context.GrupyStudenckie.FindAsync(id);

            if (grupaStudencka == null)
            {
                return NotFound();
            }

            return grupaStudencka;
        }

        // PUT: api/GrupyStudenckie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrupaStudencka(long id, GrupaStudencka grupaStudencka)
        {
            if (id != grupaStudencka.IdGrupy)
            {
                return BadRequest();
            }

            _context.Entry(grupaStudencka).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupaStudenckaExists(id))
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

        // POST: api/GrupyStudenckie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GrupaStudencka>> PostGrupaStudencka(GrupaStudencka grupaStudencka)
        {
          if (_context.GrupyStudenckie == null)
          {
              return Problem("Entity set 'UniversifyDbContext.GrupyStudenckie'  is null.");
          }
            _context.GrupyStudenckie.Add(grupaStudencka);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrupaStudencka", new { id = grupaStudencka.IdGrupy }, grupaStudencka);
        }

        // DELETE: api/GrupyStudenckie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupaStudencka(long id)
        {
            if (_context.GrupyStudenckie == null)
            {
                return NotFound();
            }
            var grupaStudencka = await _context.GrupyStudenckie.FindAsync(id);
            if (grupaStudencka == null)
            {
                return NotFound();
            }

            _context.GrupyStudenckie.Remove(grupaStudencka);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GrupaStudenckaExists(long id)
        {
            return (_context.GrupyStudenckie?.Any(e => e.IdGrupy == id)).GetValueOrDefault();
        }
    }
}
