using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Net.Codecrete.QrCodeGenerator;
using webapi.Data;
using webapi.Hubs;
using webapi.Models;
using webapi.Models.DTOs;

/*
 * LICENSE
 * 
 * Copyright (c) 2024 PI_Universify (MIT License)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice (including the next paragraph) shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace webapi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ParkingiController : ControllerBase, IListeners
    {
        private readonly UniversifyDbContext _context;
        private readonly IHubContext<ParkingHub> _parkingiHub;
        private readonly RequestTimer _timer;
        private readonly Expression<Func<Parking, ParkingDTO>> AsParkingDTO =
            p => new ParkingDTO(p);

        public ParkingiController(UniversifyDbContext context, IHubContext<ParkingHub> hub)
        {
            _context = context;
            _parkingiHub = hub;
            _timer = new RequestTimer(900);
            _timer.listeners.Add(this);
        }

        // GET: api/Parkingi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingDTO>>> GetParkingi()
        {
            if (_context.Parkingi == null)
            {
                return NotFound();
            }
            return await _context.Parkingi
                .Include(p => p.Miejsca).ThenInclude(m => m.Typ).Select(AsParkingDTO).ToListAsync();
        }

        // GET: api/Parkingi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingDTO>> GetParking(long id)
        {
            if (_context.Parkingi == null)
            {
                return NotFound();
            }
            var parking = await _context.Parkingi
                .Include(p => p.Miejsca).ThenInclude(m => m.Typ)
                .Where(p => p.IdParkingu == id).Select(AsParkingDTO).SingleAsync();

            if (parking == null)
            {
                return NotFound();
            }

            return parking;
        }

        [HttpGet("~/api/Parkingi/fullparking")]
        public async Task<ActionResult<List<Dictionary<String, dynamic>>>> GetFullParkingi()
        {
            if (_context.Parkingi == null)
            {
                return NotFound();
            }

            var parkingi = await _context.Parkingi!
                .Include(p => p.Miejsca).ThenInclude(m => m.Typ)
                .Include(p => p.RozkładParkingu).Select(AsParkingDTO).ToListAsync();


            if (parkingi == null)
            {
                return NotFound();
            }

            List<Dictionary<String, dynamic>> answer = new List<Dictionary<string, dynamic>>();

            for (int id = 0; id < parkingi.Count; id++)
            {
                //var rozkład = await _context.RozkladParkingu!.Where(e => e.IdParkingu - 1 == id).Select(r => new RozkładParkinguDTO(r)).ToListAsync();

                answer.Add(new Dictionary<string, dynamic>
                {
                    ["LiczbaRzedow"] = parkingi[id].LiczbaRzedow ?? 0,
                    ["Adres"] = parkingi[id].Adres ?? "",
                    ["Miejsca"] = parkingi[id].Miejsca ?? new List<MiejsceDTO>(),
                    ["Rozklad"] = parkingi[id].Rozkład ?? new List<RozkładParkinguDTO>(),
                });
            }

            return answer;
        }

        [HttpGet("~/api/Parkingi/fullparking/{id}")]
        public async Task<ActionResult<Dictionary<String, dynamic>>> GetFullParking(long id)
        {
            if (_context.Parkingi == null)
            {
                return NotFound();
            }

            var parking = await _context.Parkingi!
                .Include(p => p.Miejsca).ThenInclude(m => m.Typ)
                .Include(p => p.RozkładParkingu)
                .Where(p => p.IdParkingu == id).Select(AsParkingDTO).SingleAsync();

            if (parking == null)
            {
                return NotFound();
            }

            Dictionary<String, dynamic> answer = new Dictionary<String, dynamic>();

            answer["LiczbaRzedow"] = parking.LiczbaRzedow ?? 0;
            answer["Adres"] = parking.Adres ?? "";
            answer["Miejsca"] = parking.Miejsca ?? new List<MiejsceDTO>();
            answer["Rozklad"] = parking.Rozkład ?? new List<RozkładParkinguDTO>();

            return answer;
        }

        [HttpGet("~/api/Parkingi/Miejsca/{id}")]
        public async Task<ActionResult<Dictionary<String, dynamic>>> GetUpdatePlace(String id)
        {
            if (_context.Parkingi == null)
            {
                return NotFound();
            }

            if (id == "reset")
            {
                try
                {
                    var parkingi = await _context.Parkingi!
                                .Include(p => p.Miejsca).ThenInclude(m => m.Typ).Include(p => p.RozkładParkingu).ToListAsync();

                    if (parkingi == null)
                    {
                        return BadRequest();
                    }

                    //var rozkłady = await _context.RozkladParkingu!.ExecuteUpdateAsync(r => r.SetProperty(o => o.StanMiejsca, 0));

                    foreach (var parking in parkingi)
                    {
                        if (parking.RozkładParkingu == null)
                        {
                            return new Dictionary<String, dynamic>
                            {
                                ["result"] = "Error",
                                ["message"] = "RozkladParkingu = null"
                            };
                        }

                        foreach (var rozklad in parking.RozkładParkingu)
                        {
                            rozklad.StanMiejsca = 0;
                        }
                    }

                    await _context.SaveChangesAsync();
                    await _parkingiHub.Clients.All.SendAsync("LayoutUpdate");

                    return new Dictionary<String, dynamic>
                    {
                        ["result"] = "Success"
                    };
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Parkingi.Count() > 0)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                int row = Int32.TryParse(id[(id.IndexOf("_") + 1)..(id.IndexOf("_", id.IndexOf("_") + 1))], out row) ? row : -1;
                int col = Int32.TryParse(id[(id.IndexOf("_", id.IndexOf("_") + 1) + 1)..id.LastIndexOf("_")], out col) ? col : -1;
                int val = Int32.TryParse(id[(id.LastIndexOf("_") + 1)..], out val) ? val : -1;

                if (val == 1)
                {
                    return BadRequest();
                }

                int idParkingu = Int32.TryParse(id[0..(id.IndexOf("_"))], out idParkingu) ? idParkingu : -1;

                var parking = await _context.Parkingi!.Include(p => p.RozkładParkingu)
                    .Where(p => p.IdParkingu == idParkingu).SingleOrDefaultAsync();

                if (parking == null)
                {
                    return NotFound();
                }

                //int? index = (int?)(parking?.RozkładParkingu?.ElementAt(0).Id) - 1 ?? 0;

                if (parking?.LiczbaRzedow != 0)
                {
                    try
                    {
                        parking!.RozkładParkingu!.ElementAt((row * parking!.RozkładParkingu!.Count / (int)parking.LiczbaRzedow! + col)).StanMiejsca = val;
                        Console.WriteLine("Updated " + row * parking!.RozkładParkingu!.Count / (int)parking.LiczbaRzedow! + " " + col + " value to: " + Newtonsoft.Json.JsonConvert.SerializeObject(new RozkładParkingu
                        {
                            Id = parking!.RozkładParkingu!.ElementAt((row * parking!.RozkładParkingu!.Count / (int)parking.LiczbaRzedow! + col)).Id,
                            IdParkingu = parking!.RozkładParkingu!.ElementAt((row * parking!.RozkładParkingu!.Count / (int)parking.LiczbaRzedow! + col)).IdParkingu,
                            StanMiejsca = parking!.RozkładParkingu!.ElementAt((row * parking!.RozkładParkingu!.Count / (int)parking.LiczbaRzedow! + col)).StanMiejsca
                        }));
                        await _context.SaveChangesAsync();
                        // rozpoczecie naliczania czasu przy rezerwacji miejsca
                        if (val == 2)
                            _timer.startTimer(new Dictionary<String, dynamic>
                            {
                                ["row"] = row,
                                ["col"] = col,
                                ["parking"] = parking
                            });
                        //await _parkingiHub.Clients.All.SendAsync("LayoutUpdate");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ParkingExists(idParkingu))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return new Dictionary<String, dynamic>
                {
                    ["LiczbaRzedow"] = parking.LiczbaRzedow,
                    ["Adres"] = parking.Adres ?? "",
                    ["Miejsca"] = parking.Miejsca ?? new List<Miejsce>(),
                    ["Rozklad"] = parking.RozkładParkingu!.Select(r => new RozkładParkinguDTO(r))
                };
            }
        }

        [HttpGet("~/api/Parkingi/QR/{id}")]
        public async Task<ActionResult<Dictionary<String, dynamic>>> GetQrCode(string id)
        {
            if (_context.Użytkownicy == null)
            {
                return NotFound();
            }

            int user = Int32.TryParse(id[..id.IndexOf("_")], out user) ? user : -1;
            int parkingId = Int32.TryParse(id[(id.IndexOf("_") + 1)..(id.IndexOf("_", id.IndexOf("_") + 1))], out parkingId) ? parkingId : -1;
            int spotId = Int32.TryParse(id[(id.LastIndexOf("_") + 1)..], out spotId) ? spotId : -1;

            var uzytkownik = await _context.Użytkownicy.FindAsync((long)user);
            if (uzytkownik == null) { return Forbid(); }
            Parking? parking = await _context.Parkingi
                .Include(p => p.RozkładParkingu)
                .Include(p => p.KodyParkingu).SingleAsync(p => p.IdParkingu == parkingId);
            if (parking == null) { return NotFound(); }
            if (parking.RozkładParkingu == null) { return NotFound(); }
            try
            {
                Console.WriteLine("Path: " + Request.Host + " " + Request.GetDisplayUrl());
                Guid guid = Guid.NewGuid();
                var qrCode = QrCode.EncodeText("https://" + Request.Host + "/api/Parkingi/rezerwacja/" + 7, QrCode.Ecc.Medium);
                string svg = qrCode.ToSvgString(4);
                byte[] buffer = qrCode.ToBmpBitmap(4, 8);
                parking.KodyParkingu.Add(new KodParkingu { IdParkingu = parkingId, IdMiejsca = spotId, Kod = guid, Parking = parking, RozkładParkingu = parking.RozkładParkingu.Single(r => r.Id == spotId) });
                await _context.SaveChangesAsync();
                Console.WriteLine(guid.ToString());
                Console.WriteLine("https://" + Request.Host + "/api/Parkingi/rezerwacja/" + guid.ToString());
                //qrCode.SaveAsPng("test.png", 10, 3);
                //await System.IO.File.WriteAllBytesAsync(Directory.GetCurrentDirectory() + "/assets/images/test.bmp", buffer);

                return new Dictionary<String, dynamic>
                {
                    ["message"] = "Success",
                    ["data"] = buffer
                };
            }
            catch (DbUpdateConcurrencyException) { }

            return new Dictionary<String, dynamic>
            {
                ["message"] = "",
            };
        }

        [HttpGet("~/api/Parkingi/rezerwacja/{guid}")]
        public async Task<IActionResult> PutParkingSpot(string guid)
        {
            Guid id = Guid.TryParse(guid, out id) ? id : Guid.Empty;
            if (id == Guid.Empty) { return BadRequest(); }
            Parking? change = await _context.Parkingi
                .Include(p => p.KodyParkingu)
                .Include(p => p.RozkładParkingu).SingleAsync(p => p.KodyParkingu.Single(k => k.Kod == id).IdParkingu == p.IdParkingu);

            if (change == null)
            {
                return BadRequest();
            }

            if (change.RozkładParkingu == null)
            {
                return NotFound();
            }

            try
            {
                Console.WriteLine("Trying to set reserved spot to occupied");
                change.RozkładParkingu.Single(r => r.Id == change.KodyParkingu.Single(k => k.Kod == id).IdMiejsca).StanMiejsca = 1;
                _context.KodyParkingu.Remove(_context.KodyParkingu.Single(k => k.Kod == id));


                //change.KodyParkingu.Remove(change.KodyParkingu.Single(k => k.Kod == id));                
                //change.RozkładParkingu.Single(r => r.Id == change.KodyParkingu.Single(k => k.Kod == id).IdMiejsca).KodyParkingu.Remove(change.KodyParkingu.Single(k => k.Kod == id));
                await _context.SaveChangesAsync();
                // zatrzymanie czasu w przypadku zeskanowania kodu przez użytkownika i poinformowanie ewentualnych listenerów
                _timer.stopTimer();
                _timer.RequestReceived();
                await _parkingiHub.Clients.All.SendAsync("LayoutUpdate", true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(change.IdParkingu))
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

        // PUT: api/Parkingi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParking(long id, ParkingDTO parking)
        {
            Parking? change = await _context.Parkingi.FindAsync(id);
            if (change == null)
            {
                return BadRequest();
            }

            change.Adres = parking?.Adres ?? "";
            change.Miejsca = parking?.Miejsca?.Select((m, index) =>
            {
                Miejsce miejsce = new Miejsce();
                TypMiejsca typ = new TypMiejsca();
                typ.IdTypu = change.Miejsca.ElementAt(index).Typ.IdTypu;
                typ.Typ = m?.Typ?.Typ ?? "";
                typ.Miejsca = change.Miejsca;
                miejsce.IdMiejsca = change.Miejsca.ElementAt(index).IdMiejsca;
                miejsce.IdTypu = change.Miejsca.ElementAt(index).IdTypu;
                miejsce.Typ = typ;
                miejsce.Dostępność = m?.Dostępność;
                return miejsce;
            }).ToList() ?? new List<Miejsce>();

            _context.Entry(change).State = EntityState.Modified;

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

        [ApiExplorerSettings(IgnoreApi = true)]
        public Task<NoContentResult> Update()
        {
            // Nie ma potrzeby czegokolwiek robić
            return Task.FromResult(NoContent());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Reset(Object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (_context == null)
            {
                throw new ArgumentNullException(nameof(_context));
            }

            Parking parking = (Parking)(((Dictionary<String, dynamic>)item)["parking"]);
            int row = ((Dictionary<String, dynamic>)item)["row"];
            int col = ((Dictionary<String, dynamic>)item)["col"];
            try
            {
                parking!.RozkładParkingu!.ElementAt((row * parking!.RozkładParkingu!.Count / (int)parking.LiczbaRzedow! + col)).StanMiejsca = 0;
                await _context.SaveChangesAsync();
                await _parkingiHub.Clients.All.SendAsync("LayoutUpdate", false);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
