using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
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
        public async Task<ActionResult<Dictionary<string, dynamic>>> GetPrzedmiotyU(long id)
        {
            if (_context.Przedmioty == null || _context.Użytkownicy == null)
            {
                return NotFound();
            }

            try
            {
                var użytkownik = await _context.Użytkownicy
                .Where(u => u.IdUżytkownika == id)
                .SingleAsync();
                List<PrzedmiotDTO> przedmioty;
                if (użytkownik == null)
                {
                    return NotFound();
                }

                if (użytkownik.GetType().Name == "Student")
                {
                    Console.WriteLine("User is a student");
                    // przedmioty = await _context.Przedmioty
                    // .Include(p => p.Studenci)
                    // .Include(p => p.Nauczyciele)
                    // .Where(p => p.Studenci.Any(s => s.IdUżytkownika == id))
                    // .Select(p => new PrzedmiotDTO(p)).ToListAsync();
                    if (_context.Studenci == null)
                    {
                        return NotFound();
                    }

                    StudentDTO student = await _context.Studenci
                    .Where(s => s.IdUżytkownika == id)
                    .Include(u => u.Przedmioty).ThenInclude(ps => ps.Przedmiot).ThenInclude(p => p.Nauczyciele)
                    .ThenInclude(n => n.Nauczyciel).ThenInclude(n => n.Wydział)
                    .Select(s => new StudentDTO
                    {
                        Przedmioty = s.Przedmioty.Select(p => p.Przedmiot == null ? null : new PrzedmiotDTO
                        {
                            Nazwa = p.Przedmiot.Nazwa,
                            Kategoria = p.Przedmiot.Kategoria,
                            SemestrRozpoczęcia = p.Przedmiot.SemestrRozpoczęcia,
                            IlośćSemestrów = p.Przedmiot.IlośćSemestrów,
                            Studenci = p.Przedmiot.Studenci.Select(st => st.Student == null ? null : new StudentDTO(st.Student.IdUżytkownika, st.Student.Imię, st.Student.Nazwisko, st.Student.Grupa, st.Student.IdBudynku, st.Student.RokStudiów,
                            st.Student.GrupaStudencka, st.Student.KierunekStudiów, st.Student.Grupy, st.Student.Przedmioty)).ToList(),
                            Nauczyciele = p.Przedmiot.Nauczyciele.Select(n => n.Nauczyciel == null ? null : new NauczycielDTO(n.Nauczyciel.IdUżytkownika, n.Nauczyciel.Imię, n.Nauczyciel.Nazwisko, n.Nauczyciel.Grupa, n.Nauczyciel.IdBudynku,
                            n.Nauczyciel.Wydział, n.Nauczyciel.Specjalizacja, n.Nauczyciel.Grupy, n.Nauczyciel.Przedmioty)).ToList()
                        }).ToList()
                    })
                    .SingleAsync();

                    przedmioty = new List<PrzedmiotDTO>();

                    if (student.Przedmioty == null)
                    {
                        return new Dictionary<string, dynamic>
                        {
                            ["uType"] = student.GetType().Name,
                            ["subjects"] = przedmioty
                        };
                    }

                    foreach (var przedmiot in student.Przedmioty!)
                    {
                        if (przedmiot == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            przedmioty.Add(przedmiot);
                        }
                    }
                }
                else if (użytkownik.GetType().Name == "Administrator")
                {
                    Console.WriteLine("User is an admin");
                    return NoContent();
                }
                else
                {
                    if (użytkownik.GetType().Name != "Nauczyciel")
                    {
                        return NotFound();
                    }
                    Console.WriteLine("User is a teacher");

                    NauczycielDTO nauczyciel = await _context.Nauczyciele
                    .Where(n => n.IdUżytkownika == id)
                    .Include(n => n.Przedmioty).ThenInclude(np => np.Przedmiot).ThenInclude(p => p.Nauczyciele)
                    .Select(n => new NauczycielDTO
                    {
                        Przedmioty = n.Przedmioty.Select(np => np.Przedmiot == null ? null : new PrzedmiotDTO()
                        {
                            Nazwa = np.Przedmiot.Nazwa,
                            Kategoria = np.Przedmiot.Kategoria,
                            SemestrRozpoczęcia = np.Przedmiot.SemestrRozpoczęcia,
                            IlośćSemestrów = np.Przedmiot.IlośćSemestrów,
                            Nauczyciele = np.Przedmiot.Nauczyciele.Select(np => np.Nauczyciel == null ? null : new NauczycielDTO(np.Nauczyciel.IdUżytkownika, np.Nauczyciel.Imię, np.Nauczyciel.Nazwisko, np.Nauczyciel.Grupa, np.Nauczyciel.IdBudynku,
                            np.Nauczyciel.Wydział, np.Nauczyciel.Specjalizacja, np.Nauczyciel.Grupy, np.Nauczyciel.Przedmioty)).ToList()
                        }).ToList()
                    }).SingleAsync();

                    przedmioty = new List<PrzedmiotDTO>();

                    if (nauczyciel.Przedmioty == null)
                    {
                        return new Dictionary<string, dynamic>
                        {
                            ["uType"] = nauczyciel.GetType().Name,
                            ["subjects"] = przedmioty
                        };
                    }

                    foreach (var przedmiot in nauczyciel.Przedmioty!)
                    {
                        if (przedmiot == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            przedmioty.Add(przedmiot);
                        }
                    }
                }


                if (przedmioty == null)
                {
                    return NotFound();
                }

                return new Dictionary<string, dynamic>
                {
                    ["uType"] = użytkownik.GetType().Name,
                    ["subjects"] = przedmioty
                };

            }
            catch (System.Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
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
                PrzedmiotStudent student = new PrzedmiotStudent();
                GrupaStudencka gs = new GrupaStudencka();
                KierunekStudiów ks = new KierunekStudiów();
                gs.IdGrupy = change.Studenci.ElementAt(index).Student?.GrupaStudencka?.IdGrupy ?? -1;
                gs.Nazwa = s?.GrupaStudencka?.Nazwa ?? "";
                gs.Studenci = change.Studenci.ElementAt(index).Student?.GrupaStudencka?.Studenci ?? new List<Student>();
                ks.IdKierunkuStudiów = change.Studenci.ElementAt(index).Student?.KierunekStudiów?.IdKierunkuStudiów ?? -1;
                ks.NazwaKierunku = s?.KierunekStudiów?.NazwaKierunku ?? "";
                ks.Studenci = change.Studenci.ElementAt(index).Student?.KierunekStudiów?.Studenci ?? new List<Student>();
                student.Student.IdStudenta = change.Studenci.ElementAt(index).Student.IdStudenta;
                student.Student.IdGrupyStudenckiej = change.Studenci.ElementAt(index).Student.IdGrupyStudenckiej;
                student.Student.IdKierunkuStudiów = change.Studenci.ElementAt(index).Student.IdKierunkuStudiów;
                student.Student.IdUżytkownika = change.Studenci.ElementAt(index).Student.IdUżytkownika;
                student.Student.GrupaStudencka = gs;
                student.Student.KierunekStudiów = ks;
                student.Student.RokStudiów = s?.RokStudiów ?? -1; // -1 oznaczające, że rok studiów = null
                student.Student.Grupy = change.Studenci.ElementAt(index).Student.Grupy;
                student.Student.Przedmioty = change.Studenci.ElementAt(index).Student.Przedmioty;
                //student.Użytkownik = change.Studenci.ElementAt(index).Użytkownik;

                return student;
            }).ToList() ?? new List<PrzedmiotStudent>();

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
