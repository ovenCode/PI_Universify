﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UprawnieniaController : ControllerBase
    {
        private readonly UniversifyDbContext _context;

        public UprawnieniaController(UniversifyDbContext context)
        {
            _context = context;
        }

        // GET: api/Uprawnienia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetUprawnienia()
        {
            if (_context.Uprawnienia == null)
            {
                return NotFound();
            }

            // Uprawnienia mają być posortowane tak, aby ZX, DX, MX, UX, ZY, DY, MY, UY
            Regex sort = new Regex(@"[Z,D,M,U]\w+"); // .OrderBy(u => sort.Match(u.Nazwa).Value)
            List<char> sortOrder = new List<char> { 'Z', 'D', 'M', 'U' };
            System.Console.WriteLine(sort.Match("ZU").Value);
            return await _context.Uprawnienia.OrderBy(u => u.Nazwa.Substring(1)).ThenBy(u => "Z").ThenBy(u => "D").ThenBy(u => "M").ThenBy(u => "U").ToListAsync();
        }

        // GET: api/Uprawnienia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Uprawnienie>> GetUprawnienie(long id)
        {
            if (_context.Uprawnienia == null)
            {
                return NotFound();
            }
            var uprawnienie = await _context.Uprawnienia.FindAsync(id);

            if (uprawnienie == null)
            {
                return NotFound();
            }

            return uprawnienie;
        }

        // PUT: api/Uprawnienia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUprawnienie(long id, Uprawnienie uprawnienie)
        {
            if (id != uprawnienie.IdUprawnienia)
            {
                return BadRequest();
            }

            _context.Entry(uprawnienie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UprawnienieExists(id))
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

        // POST: api/Uprawnienia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Uprawnienie>> PostUprawnienie(Uprawnienie uprawnienie)
        {
            if (_context.Uprawnienia == null)
            {
                return Problem("Entity set 'UniversifyDbContext.Uprawnienia'  is null.");
            }
            _context.Uprawnienia.Add(uprawnienie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUprawnienie", new { id = uprawnienie.IdUprawnienia }, uprawnienie);
        }

        // DELETE: api/Uprawnienia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUprawnienie(long id)
        {
            if (_context.Uprawnienia == null)
            {
                return NotFound();
            }
            var uprawnienie = await _context.Uprawnienia.FindAsync(id);
            if (uprawnienie == null)
            {
                return NotFound();
            }

            _context.Uprawnienia.Remove(uprawnienie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UprawnienieExists(long id)
        {
            return (_context.Uprawnienia?.Any(e => e.IdUprawnienia == id)).GetValueOrDefault();
        }
    }
}
