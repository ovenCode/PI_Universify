using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using webapi.Data;
using webapi.Models;
using webapi.Models.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzytkownicyController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        private readonly Expression<Func<Użytkownik, UżytkownikDTO>> AsUzytkownikDTO = 
            user => new UżytkownikDTO
            {
                IdUżytkownika = user.IdUżytkownika,
                Imię = user.Imię,
                Nazwisko = user.Nazwisko,
                Grupa = user.Grupa,
                Budynek = user.Budynek,
                Administrator = new AdministratorDTO(user.Administrator), //  != null ? new Administrator { IdAdministratora = user?.Administrator?.IdAdministratora ?? Convert.ToInt64(0), IdUżytkownika = user?.Administrator?.IdUżytkownika ?? Convert.ToInt64(0), IdRoli = user?.Administrator?.IdRoli ?? Convert.ToInt64(0), Rola = user?.Administrator?.Rola ?? new Rola() } : 
                Nauczyciel = new NauczycielDTO(user.Nauczyciel), // != null ? new Nauczyciel { IdNauczyciela = user?.Nauczyciel?.IdNauczyciela ?? Convert.ToInt64(0), IdUżytkownika = user?.Nauczyciel?.IdUżytkownika ?? Convert.ToInt64(0), IdWydziału = user?.Nauczyciel?.IdWydziału ?? Convert.ToInt64(0), IdSpecjalizacji = user?.Nauczyciel?.IdSpecjalizacji ?? Convert.ToInt64(0), Specjalizacja = user?.Nauczyciel?.Specjalizacja ?? new Specjalizacja() },
                Student = new StudentDTO(user.Student) // != null ? new Student { IdStudenta = user?.Student?.IdStudenta ?? Convert.ToInt64(0), IdUżytkownika = user?.Student?.IdUżytkownika ?? Convert.ToInt64(0), IdPrzedmiotu = user?.Student?.IdPrzedmiotu ?? Convert.ToInt64(0), IdGrupyStudenckiej = user?.Student?.IdGrupyStudenckiej ?? Convert.ToInt64(0), IdKierunkuStudiów = user?.Student?.IdKierunkuStudiów ?? Convert.ToInt64(0), RokStudiów = user?.Student?.RokStudiów ?? Convert.ToInt64(0), GrupaStudencka = user?.Student?.GrupaStudencka ?? new GrupaStudencka(), KierunekStudiów = user?.Student?.KierunekStudiów ?? new KierunekStudiów() }
            };
        public UzytkownicyController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Uzytkownicy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UżytkownikDTO>>> GetUżytkownicy()
        {
            if (_context.Użytkownicy == null)
            {
                return NotFound();
            }

            IQueryable<Użytkownik> response = _context.Użytkownicy;

            /*
             * await _context.Użytkownicy
                .Include(u => u.Student)
                .Include(u => u.Nauczyciel)
                .Include(u => u.Administrator)
                .Select(AsUzytkownikDTO).ToListAsync()
             */

            foreach (Użytkownik item in response) {
                _context.Entry(item)
                    .Reference(u => u.Student)
                    .Query()                    
                    .Include(s => s.Przedmioty)
                    .ThenInclude(p => p.Nauczyciele)                    
                    .Load();

                _context.Entry(item)
                    .Reference(u => u.Nauczyciel)
                    .Query()
                    .Include(n => n.Przedmioty)
                    .Load();

                _context.Entry(item)
                    .Reference(u => u.Administrator)
                    .Load();
            }
            return await response.Select(AsUzytkownikDTO).ToListAsync();
        }

        // GET: api/Uzytkownicy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UżytkownikDTO>> GetUżytkownik(long id)
        {
            if (_context.Użytkownicy == null)
            {
                return NotFound();
            }

            var użytkownik = await _context.Użytkownicy
                .Include(u => u.Student).ThenInclude(s => s.GrupaStudencka)
                .Include(u => u.Student).ThenInclude(s => s.KierunekStudiów)
                .Include(u => u.Student).ThenInclude(s => s.Przedmioty).ThenInclude(p => p.Nauczyciele)
                .Include(u => u.Student).ThenInclude(s => s.Grupy)
                .Include(u => u.Nauczyciel).ThenInclude(n => n.Przedmioty)
                .Include(u => u.Nauczyciel).ThenInclude(n => n.Specjalizacja)
                .Include(u => u.Administrator).ThenInclude(a => a.Rola)
                .Where(u => u.IdUżytkownika == id).Select(AsUzytkownikDTO).SingleAsync();

            /*foreach (Użytkownik item in response)
            {
                _context.Entry(item)
                    .Reference(u => u.Student)
                    .Query()
                    .Include(s => s.Przedmioty)
                    .ThenInclude(p => p.Nauczyciele)
                    .Include(s => s.GrupaStudencka)
                    .ThenInclude(g => g.Nazwa)
                    .Include(s => s.KierunekStudiów)
                    .ThenInclude(k => k.NazwaKierunku)
                    .Include(s => s.Grupy)
                    .ThenInclude(g => g.Nazwa)
                    .Load();

                _context.Entry(item)
                    .Reference(u => u.Nauczyciel)
                    .Query()
                    .Include(n => n.Przedmioty)
                    .Load();

                _context.Entry(item)
                    .Reference(u => u.Administrator)
                    .Load();
            }*/

            //var użytkownik = await response.Select(AsUzytkownikDTO).SingleAsync();

            if (użytkownik == null)
            {
                return NotFound();
            }

            return użytkownik;
        }

        // PUT: api/Uzytkownicy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUżytkownik(long id, Użytkownik użytkownik)
        {
            if (id != użytkownik.IdUżytkownika)
            {
                return BadRequest();
            }

            _context.Entry(użytkownik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UżytkownikExists(id))
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

        // POST: api/Uzytkownicy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Użytkownik>> PostUżytkownik(Użytkownik użytkownik)
        {
            if (_context.Użytkownicy == null)
            {
                return Problem("Entity set 'UniversifyDbContext.Użytkownicy'  is null.");
            }
            _context.Użytkownicy.Add(użytkownik);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUżytkownik), new { id = użytkownik.IdUżytkownika }, użytkownik);
        }

        // POST: api/Uzytkownicy/login
        [HttpPost("login")]
        public async Task<ActionResult<String[]>> Login([FromBody] JsonElement loginData)
        {
            // TODO: Linia do usunięcia, DANE UŻYTKOWNIKA
            Console.WriteLine(loginData.ToString());
            //Dictionary<String, dynamic>? loginInfo = JsonSerializer.Deserialize<Dictionary<String, dynamic>>(loginData);            
            Dictionary<string, string>? loginInfo = new Dictionary<string, string>();
            loginInfo["username"] = JsonSerializer.Serialize(loginData).Trim('{','}',' ').Substring(JsonSerializer.Serialize(loginData).Trim('{', '}', ' ').IndexOf(":") + 2, JsonSerializer.Serialize(loginData).Trim('{', '}', ' ').IndexOf(",") - JsonSerializer.Serialize(loginData).Trim('{', '}', ' ').IndexOf(":") - 3) ?? "";
            loginInfo["password"] = JsonSerializer.Serialize(loginData).Trim('{','}',' ').Substring(JsonSerializer.Serialize(loginData).Trim('{', '}', ' ').LastIndexOf(":") + 2, JsonSerializer.Serialize(loginData).Trim('{','}',' ').Length - JsonSerializer.Serialize(loginData).Trim('{', '}', ' ').LastIndexOf(":") - 3) ?? "";
            /*loginInfo["username"] = (string)loginData["username"] ?? "";
            loginInfo["password"] = (string)loginData["password"] ?? "";*/
            // user.Mail == (loginInfo?["username"] ?? "") && user.Hasło == (loginInfo?["password"] ?? "")
            if(_context.Użytkownicy == null || loginInfo == null)
            {
                return new string[] { "false", "-2" };
            }

            try
            {
                String username = loginInfo["username"], password = loginInfo["password"].ToString();
                bool isLoggedIn = await _context.Użytkownicy!.AnyAsync(user => user.Mail == username && user.Hasło == password);
                long? userId = (await _context.Użytkownicy.FirstAsync(user => user.Mail == username && user.Hasło == password)).IdUżytkownika;


                return new string[] { isLoggedIn.ToString(), userId.ToString() ?? "-1" };
            }
            catch (Exception)
            {
                // TODO: Poprawić error handling przy logowaniu
                return NotFound();
            }
        }

        // DELETE: api/Uzytkownicy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUżytkownik(long id)
        {
            if (_context.Użytkownicy == null)
            {
                return NotFound();
            }
            var użytkownik = await _context.Użytkownicy.FindAsync(id);
            if (użytkownik == null)
            {
                return NotFound();
            }

            _context.Użytkownicy.Remove(użytkownik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UżytkownikExists(long id)
        {
            return (_context.Użytkownicy?.Any(e => e.IdUżytkownika == id)).GetValueOrDefault();
        }

        /*public static UżytkownikDTO ItemToDTO(Użytkownik user, Administrator? admin = null, Rola? rola = null, Nauczyciel? teacher = null, Specjalizacja? specjalizacja = null, Student? student = null, GrupaStudencka? grupaStudencka = null, KierunekStudiów? kierunekStudiów = null) => new UżytkownikDTO
        {
            IdUżytkownika = user.IdUżytkownika,
            Imię = user.Imię,
            Nazwisko = user.Nazwisko,
            Grupa = user.Grupa,
            Budynek = user.Budynek,
            Administrator = new Administrator { IdAdministratora = admin?.IdAdministratora ?? Convert.ToInt64(0), IdUżytkownika = admin?.IdUżytkownika ?? Convert.ToInt64(0), IdRoli = admin?.IdRoli ?? Convert.ToInt64(0), Rola = rola ?? new Rola() },
            Nauczyciel = new Nauczyciel { IdNauczyciela = teacher?.IdNauczyciela ?? Convert.ToInt64(0), IdUżytkownika = teacher?.IdUżytkownika ?? Convert.ToInt64(0), IdWydziału = teacher?.IdWydziału ?? Convert.ToInt64(0), IdSpecjalizacji = teacher?.IdSpecjalizacji ?? Convert.ToInt64(0), Specjalizacja = specjalizacja ?? new Specjalizacja() },
            Student = new Student { IdStudenta = user?.Student?.IdStudenta ?? Convert.ToInt64(0), IdUżytkownika = user?.Student?.IdUżytkownika ?? Convert.ToInt64(0), IdPrzedmiotu = user?.Student?.IdPrzedmiotu ?? Convert.ToInt64(0), IdGrupyStudenckiej = user?.Student?.IdGrupyStudenckiej ?? Convert.ToInt64(0), IdKierunkuStudiów = user?.Student?.IdKierunkuStudiów ?? Convert.ToInt64(0), RokStudiów = user?.Student?.RokStudiów ?? Convert.ToInt64(0), GrupaStudencka = grupaStudencka ?? new GrupaStudencka(), KierunekStudiów = kierunekStudiów ?? new KierunekStudiów() }
        };*/
    }
}
