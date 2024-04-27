using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.Models.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public ProfileController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dictionary<String, dynamic>>>> GetProfil()
        {
            if (_context.Profile == null)
            {
                return NotFound();
            }
            return await GetProfileInfo() ?? NotFound("No profiles were found");
        }

        // GET: api/Profile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dictionary<String, dynamic>>> GetProfil(long id)
        {
            if (_context.Profile == null)
            {
                return NotFound("Brak profili. Skontaktuj się z administratorem uczelni.");
            }
            var profil = await GetProfileInfo(id);

            if (profil == null)
            {
                return NotFound("Nie znaleziono profilu. Skontaktuj się z administratorem uczelni.");
                //Problem(statusCode: StatusCodes.Status404NotFound, detail: "Nie znaleziono profilu. Skontaktuj się z administratorem uczelni.");
            }

            //IEnumerable<Dictionary<String, dynamic>> profileInfo = await GetProfileInfo(id);
            //List<Dictionary<String, dynamic>> answer = profileInfo.ToList();
            var answer = await GetProfileInfo(id);

            return answer!.Value!.ElementAt(0);
        }

        // PUT: api/Profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfil(long id, Profil profil)
        {
            if (id != profil.IdProfilu)
            {
                return BadRequest();
            }

            _context.Entry(profil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilExists(id))
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

        // POST: api/Profile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profil>> PostProfil(Profil profil)
        {
            if (_context.Profile == null)
            {
                return Problem("Entity set 'UniversifyDbContext.Profil'  is null.");
            }
            _context.Profile.Add(profil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfil", new { id = profil.IdProfilu }, profil);
        }

        // DELETE: api/Profile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfil(long id)
        {
            if (_context.Profile == null)
            {
                return NotFound();
            }
            var profil = await _context.Profile.FindAsync(id);
            if (profil == null)
            {
                return NotFound();
            }

            _context.Profile.Remove(profil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfilExists(long id)
        {
            return (_context.Profile?.Any(e => e.IdProfilu == id)).GetValueOrDefault();
        }

        private async Task<ActionResult<IEnumerable<Dictionary<String, dynamic>>>?> GetProfileInfo(long id = -1)
        {
            if (id != -1)
            {
#pragma warning disable CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
                //var profil = await _context.Profile.Select(p => new ProfilDTO() { IdProfilu = p.IdProfilu, ObrazProfilu = p.ObrazProfilu, Użytkownik = UzytkownicyController.ItemToDTO(p.Użytkownik, p.Użytkownik.Administrator, p.Użytkownik.Administrator.Rola, p.Użytkownik.Nauczyciel, p.Użytkownik.Nauczyciel.Specjalizacja, p.Użytkownik.Student, p.Użytkownik.Student.GrupaStudencka, p.Użytkownik.Student.KierunekStudiów), }).SingleAsync(p => p.IdProfilu == id);
                var profil = await _context.Profile.Include(p => p.Użytkownik).SingleOrDefaultAsync(p => p.IdProfilu == id);
                /*.Include(p => p.Użytkownik).ThenInclude(p => p.Administrator).ThenInclude(p => p.Rola)
                .Include(p => p.Użytkownik).ThenInclude(p => p.Student).ThenInclude(p => p.GrupaStudencka)
                .Include(p => p.Użytkownik).ThenInclude(p => p.Student).ThenInclude(p => p.KierunekStudiów)
                .Include(p => p.Użytkownik).ThenInclude(p => p.Nauczyciel).ThenInclude(p => p.Specjalizacja).SingleOrDefaultAsync(p => p.IdProfilu == id);*/
#pragma warning restore CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
                if (profil == null)
                {
                    return null;
                }
                else
                {
                    if (profil.Użytkownik.GetType() == typeof(Student))
                    {
                        // Student
                        profil = await _context.Profile
                            .Where(p => p.IdProfilu == id)
                            .Include(p => p.Użytkownik).ThenInclude(u => ((Student)u).GrupaStudencka)
                            .Include(p => p.Użytkownik).ThenInclude(u => ((Student)u).KierunekStudiów)
                            .SingleAsync();

                    }
                    else if (profil.Użytkownik.GetType() == typeof(Nauczyciel))
                    {
                        // Nauczyciel
                        profil = await _context.Profile
                            .Where(p => p.IdProfilu == id)
                            .Include(p => p.Użytkownik).ThenInclude(u => ((Nauczyciel)u).Specjalizacja)
                            .Include(p => p.Użytkownik).ThenInclude(u => ((Nauczyciel)u).Wydział)
                            .SingleAsync();
                    }
                    else
                    {
                        // Administrator
                        profil = await _context.Profile
                            .Where(p => p.IdProfilu == id)
                            .Include(p => p.Użytkownik).ThenInclude(u => ((Administrator)u).Rola)
                            .SingleAsync();
                    }
                }
                Dictionary<String, dynamic> profilMap = new Dictionary<string, dynamic>();
                List<Dictionary<String, dynamic>> answer = new List<Dictionary<string, dynamic>>();
                profilMap["picture"] = profil.ObrazProfilu;
                profilMap["info"] = new Dictionary<String, dynamic>()
                    {
                        {"name", profil.Użytkownik.Imię},
                        {"lastname", profil.Użytkownik.Nazwisko},
                        {"mail", profil.Użytkownik.Mail}
                    };

                if (profil.Użytkownik.GetType().Name == "Adminstrator")
                {
                    profilMap["info"].Add("group", ((Administrator)profil.Użytkownik).Rola != null ? ((Administrator)profil.Użytkownik).Rola.Nazwa : ((Administrator)profil.Użytkownik).IdRoli.ToString());
                    profilMap["info"].Add("curriculum", "");

                }
                else if (profil.Użytkownik.GetType().Name == "Nauczyciel")
                {
                    profilMap["info"].Add("group", ((Nauczyciel)profil.Użytkownik).Specjalizacja != null ? ((Nauczyciel)profil.Użytkownik).Specjalizacja.Nazwa : ((Nauczyciel)profil.Użytkownik).IdSpecjalizacji.ToString());
                    profilMap["info"].Add("curriculum", "");
                }
                else if (profil.Użytkownik.GetType().Name == "Student")
                {
                    profilMap["info"].Add("group", ((Student)profil.Użytkownik).GrupaStudencka.Nazwa);
                    profilMap["info"].Add("curriculum", ((Student)profil.Użytkownik).KierunekStudiów.NazwaKierunku);
                }
                profilMap["sidebar"] = new Dictionary<String, dynamic>() {
                    {"info", ""}
                 };
                profilMap["content"] = new Dictionary<String, dynamic>()
                {
                    // DON'T KNOW YET
                    {"calendar",new Dictionary<String, dynamic>()
                    {
                        {"name","Some calendar"},
                        {"currentMonth", DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month)},
                        {
                            "months", Enumerable.Range(1, 12)
                            .Select(month => new Dictionary<string, dynamic>()
                            {
                                { "name", DateTimeFormatInfo.CurrentInfo.GetMonthName(month) },
                                { "days", Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, month))
                                    .Select(day => new Dictionary<string, dynamic>() { { "number", day }, { "upcoming", new List<JsonObject>() { new JsonObject(new KeyValuePair<string,JsonNode?>[] { new KeyValuePair<string, JsonNode?>("name", ""), new KeyValuePair<string, JsonNode?>("date", $"{day}/{month}") }) } } })
                                    .ToList()
                                }
                            }).ToList()
                        }
                    }},
                    {"upcoming", new List<String>() {""}},
                    {"feed", new List<String>() {""}}
                };
                answer.Add(new Dictionary<string, dynamic>(profilMap));

                return answer;
            }
            else
            {

                Dictionary<String, dynamic> profileMap = new Dictionary<string, dynamic>();
                List<Dictionary<String, dynamic>> profileInfo = new List<Dictionary<string, dynamic>>();

#pragma warning disable CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
                var profile = await _context.Profile.Select(p => new { p.ObrazProfilu, p.Użytkownik, ((Administrator)p.Użytkownik).Rola, ((Nauczyciel)p.Użytkownik).Specjalizacja, ((Student)p.Użytkownik).GrupaStudencka, ((Student)p.Użytkownik).KierunekStudiów }).ToListAsync();
#pragma warning restore CS8602 // Wyłuskanie odwołania, które może mieć wartość null.

                if (profile == null)
                {
                    return null;
                }

                //List<Użytkownik> użytkownicy = await _context.Użytkownicy.ToListAsync();
                //List<Student> studenci = await _context.Studenci.ToListAsync();
                List<Nauczyciel> nauczyciele = await _context.Nauczyciele.ToListAsync();
                List<Administrator> administratorzy = await _context.Administratorzy.ToListAsync();

                for (int i = 0; i < profile.Count; i++)
                {
                    profileMap["picture"] = profile[i].ObrazProfilu;
                    profileMap["info"] = new Dictionary<String, dynamic>()
                {
                    {"name", profile[i].Użytkownik.Imię},
                    {"lastname", profile[i].Użytkownik.Nazwisko},
                    {"mail", profile[i].Użytkownik.Mail},
                    {"group", profile[i].Użytkownik.GetType().Name == "Student" ? ((Student)profile[i]!.Użytkownik!).GrupaStudencka.Nazwa : (profile[i].Użytkownik.GetType().Name == "Nauczyciel" ? ((Nauczyciel)profile[i]!.Użytkownik!).Specjalizacja : ((Administrator) profile[i]!.Użytkownik!).Rola.Nazwa) }, //studenci.Where(s => s.IdUżytkownika == i + 1).IsNullOrEmpty() ? (nauczyciele.Where(n => n.IdUżytkownika == i + 1).IsNullOrEmpty() ? administratorzy.Where(n => n.IdUżytkownika == i + 1).First().IdRoli : nauczyciele.Where(n => n.IdUżytkownika == i + 1).First().IdSpecjalizacji ) : studenci[i].GrupaStudencka },
                    {"curriculum", profile[i].Użytkownik.GetType().Name != "Student" ? ((Student)profile[i]!.Użytkownik!).KierunekStudiów.NazwaKierunku : ""}//studenci.Where(s => s.IdUżytkownika == i + 1).IsNullOrEmpty() ? "" : studenci[i].KierunekStudiów }
                };
                    profileMap["sidebar"] = new Dictionary<String, dynamic>()
                    {
                        // DON'T KNOW YET
                        {"info", ""}
                    };
                    profileMap["content"] = new Dictionary<String, dynamic>()
                {
                    // DON'T KNOW YET
                    {"calendar",new Dictionary<String, dynamic>()
                    {
                        {"name",""},
                        {"currentMonth", DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month)},
                        {
                            "months", Enumerable.Range(1, 12)
                            .Select(month => new Dictionary<string, dynamic>()
                            {
                                { "name", DateTimeFormatInfo.CurrentInfo.GetMonthName(month) },
                                { "days", Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, month))
                                    .Select(day => new Dictionary<string, dynamic>() { { "number", day }, { "upcoming", new List<JsonObject>() { new JsonObject(new KeyValuePair<string,JsonNode?>[] { new KeyValuePair<string, JsonNode?>("name", ""), new KeyValuePair<string, JsonNode?>("date", $"{day}/{month}") }) } } })
                                    .ToList()
                                }
                            }).ToList()
                        }
                    }},
                    {"upcoming", new List<String>() { "" }},
                    {"feed", new List<String>() { "" }}
                };

                    profileInfo.Add(new Dictionary<string, dynamic>(profileMap));
                }

                return profileInfo;
            }
        }

        /*private static ProfilDTO ItemToDTO(Profil profil) => new ProfilDTO
        {
            IdProfilu = profil.IdProfilu,
            IdUżytkownika = profil.IdUżytkownika,
            ObrazProfilu = profil.ObrazProfilu,
            PasekProfilu = profil.PasekProfilu,
            GłównaZawartość = profil.GłównaZawartość,
            Użytkownik = UzytkownicyController.ItemToDTO(profil.Użytkownik)
        };*/
    }
}
