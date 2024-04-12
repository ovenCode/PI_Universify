using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.Models.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrzedmiotyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        private readonly Expression<Func<Przedmiot, PrzedmiotDTO>> AsPrzedmiotDTO = 
            przedmiot => new PrzedmiotDTO(przedmiot);

        public PrzedmiotyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Przedmioty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrzedmiotDTO>>> GetPrzedmioty()
        {
          if (_context.Przedmioty == null)
          {
              return NotFound();
          }
            return await _context.Przedmioty.Select(AsPrzedmiotDTO).ToListAsync();
        }

        // GET: api/Przedmioty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrzedmiotDTO>> GetPrzedmiot(long id)
        {
          if (_context.Przedmioty == null)
          {
              return NotFound();
          }
            var przedmiot = await _context.Przedmioty.Where(p => p.IdPrzedmiotu == id).Select(AsPrzedmiotDTO).SingleAsync();

            if (przedmiot == null)
            {
                return NotFound();
            }

            return przedmiot;
        }

        // GET: api/Przedmioty/Użytkownik/5
        [HttpGet("~/api/Przedmioty/Użytkownik/{id}")]
        public async Task<ActionResult<IEnumerable<PrzedmiotDTO>>> GetPrzedmiotyU(long id)
        {
            if(_context.Przedmioty == null)
            {
                return NotFound();
            }

            var przedmioty = await _context.Studenci
                .Include(s => s.Przedmioty)
                .Where(s => s.IdUżytkownika == id)
                .Select(s => s.Przedmioty.AsQueryable().Select(AsPrzedmiotDTO).ToList()).SingleAsync();

            if(przedmioty == null)
            {
                return NotFound();
            }

            return przedmioty;
        }

        // PUT: api/Przedmioty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrzedmiot(long id, PrzedmiotDTO przedmiot)
        {
            Przedmiot? change = await _context.Przedmioty.FindAsync(id);
            if (change == null)
            {
                return BadRequest();
            }

            change.Nazwa = przedmiot.Nazwa;
            change.Kategoria = przedmiot.Kategoria;
            change.SemestrRozpoczęcia = przedmiot.SemestrRozpoczęcia ?? 0;
            change.IlośćSemestrów = przedmiot.IlośćSemestrów ?? 0;
            change.Studenci = przedmiot?.Studenci?.Select((s, index) =>
            {
                Student student = new Student();
                GrupaStudencka gs = new GrupaStudencka();
                KierunekStudiów ks = new KierunekStudiów();
                gs.IdGrupy = change.Studenci.ElementAt(index).GrupaStudencka.IdGrupy;
                gs.Nazwa = s.GrupaStudencka.Nazwa;
                gs.Studenci = change.Studenci.ElementAt(index).GrupaStudencka.Studenci;
                ks.IdKierunkuStudiów = change.Studenci.ElementAt(index).KierunekStudiów.IdKierunkuStudiów;
                ks.NazwaKierunku = s.KierunekStudiów.NazwaKierunku;
                ks.Studenci = change.Studenci.ElementAt(index).KierunekStudiów.Studenci;
                student.IdStudenta = change.Studenci.ElementAt(index).IdStudenta;
                student.IdGrupyStudenckiej = change.Studenci.ElementAt(index).IdGrupyStudenckiej;
                student.IdKierunkuStudiów = change.Studenci.ElementAt(index).IdKierunkuStudiów;
                student.IdUżytkownika = change.Studenci.ElementAt(index).IdUżytkownika;
                student.GrupaStudencka = gs;
                student.KierunekStudiów = ks;
                student.RokStudiów = s.RokStudiów ?? -1; // -1 oznaczające, że rok studiów = null
                student.Grupy = change.Studenci.ElementAt(index).Grupy;
                student.Przedmioty = change.Studenci.ElementAt(index).Przedmioty;
                student.Użytkownik = change.Studenci.ElementAt(index).Użytkownik;

                return student;
            }).ToList() ?? new List<Student>();

            _context.Entry(change).State = EntityState.Modified;

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

            /*Przedmiot added = new Przedmiot
            {
                IdPrzedmiotu = (await _context.Przedmioty.LastAsync())!.IdPrzedmiotu + 1,
                Nazwa = przedmiot.Nazwa,
                SemestrRozpoczęcia = (long)przedmiot.SemestrRozpoczęcia!,
                IlośćSemestrów = (long)przedmiot.IlośćSemestrów!,
                Nauczyciele = (await _context.Nauczyciele.Include(n => n.Przedmioty).Where(n => n.IdPrzedmiotu == (_context.Przedmioty.Find())!.IdPrzedmiotu + 1).ToListAsync()),
                Studenci = (await _context.Studenci.Include(s => s.Przedmioty).Where(s => s.IdPrzedmiotu == (_context.Przedmioty.Find())!.IdPrzedmiotu + 1).ToListAsync())
            };*/
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
