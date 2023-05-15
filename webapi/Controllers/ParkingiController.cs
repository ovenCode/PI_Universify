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
    public class ParkingiController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public ParkingiController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Parkingi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParkingi()
        {
          if (_context.Parkingi == null)
          {
              return NotFound();
          }
            return await _context.Parkingi.ToListAsync();
        }

        // GET: api/Parkingi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parking>> GetParking(long id)
        {
          if (_context.Parkingi == null)
          {
              return NotFound();
          }
            var parking = await _context.Parkingi.FindAsync(id);

            if (parking == null)
            {
                return NotFound();
            }

            return parking;
        }

        // PUT: api/Parkingi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParking(long id, Parking parking)
        {
            if (id != parking.IdParkingu)
            {
                return BadRequest();
            }

            _context.Entry(parking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(id))
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

        // POST: api/Parkingi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parking>> PostParking(Parking parking)
        {
          if (_context.Parkingi == null)
          {
              return Problem("Entity set 'UniversifyDbContext.Parkingi'  is null.");
          }
            _context.Parkingi.Add(parking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParking", new { id = parking.IdParkingu }, parking);
        }

        // DELETE: api/Parkingi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParking(long id)
        {
            if (_context.Parkingi == null)
            {
                return NotFound();
            }
            var parking = await _context.Parkingi.FindAsync(id);
            if (parking == null)
            {
                return NotFound();
            }

            _context.Parkingi.Remove(parking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingExists(long id)
        {
            return (_context.Parkingi?.Any(e => e.IdParkingu == id)).GetValueOrDefault();
        }
    }
}
