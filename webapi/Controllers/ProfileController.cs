using System;
using System.Collections.Generic;
using System.Globalization;
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
            return await GetProfileInfo();
        }

        // GET: api/Profile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Dictionary<String, dynamic>>>> GetProfil(long id)
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

            return await GetProfileInfo();
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

        private async Task<ActionResult<IEnumerable<Dictionary<String, dynamic>>>> GetProfileInfo(long id = -1)
        {

            Dictionary<String, dynamic> profileMap = new Dictionary<string, dynamic>();
            List<Dictionary<String, dynamic>> profileInfo = new List<Dictionary<string, dynamic>>();

            List<Profil> profile = await _context.Profile.ToListAsync();
            List<Użytkownik> użytkownicy = await _context.Użytkownicy.ToListAsync();
            List<Student> studenci = await _context.Studenci.ToListAsync();

            for (int i = 0; i < profile.Count; i++)
            {
                profileMap["picture"] = profile[i].ObrazProfilu;
                profileMap["info"] = new Dictionary<String, dynamic>()
                {
                    {"name", użytkownicy[i].Imię},
                    {"lastname", użytkownicy[i].Nazwisko},
                    {"mail", użytkownicy[i].Mail},
                    {"group", studenci[i].GrupaStudencka}, // NEEDS TO CHANGE WHEN ADDING USERS OTHER THAN STUDENTS
                    {"curriculum", studenci[i].KierunekStudiów}
                };
                profileMap["sidebar"] = new Dictionary<String, dynamic>()
                {
                    // DON'T KNOW YET
                };
                profileMap["content"] = new Dictionary<String, dynamic>()
                {
                    // DON'T KNOW YET
                    {"calendar",new Dictionary<String, dynamic>()
                    {
                        {"name",""},
                        {"currentMonth", DateTime.Now.Month},
                        {
                            "months", Enumerable.Range(1, DateTimeFormatInfo.CurrentInfo.MonthNames.Length)
                            .Select(month => new Dictionary<string, dynamic>()
                            {
                                { "name", DateTimeFormatInfo.CurrentInfo.GetMonthName(month) },
                                { "days", Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, month))
                                    .Select(day => new Dictionary<string, dynamic>() { { "number", day } })
                                    .ToList()
                                }
                            }).ToList()
                        }
                    }}
                };

                profileInfo.Add(profileMap);
            }

            if (id != -1)
            {
                profileInfo = new List<Dictionary<String, dynamic>>() { profileInfo[Convert.ToInt32(id)] };
            }

            return profileInfo;
        }
    }
}
