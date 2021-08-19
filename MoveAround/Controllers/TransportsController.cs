using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoveAround.Data;
using MoveAround.Models;

namespace MoveAround.Controllers
{
    public class TransportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transport.Include(t => t.Uzsakovas);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport
                .Include(t => t.Uzsakovas)
                .FirstOrDefaultAsync(m => m.TransportId == id);
            if (transport == null)
            {
                return NotFound();
            }

            if (transport.UzsakovasId != GautiAppUser().Id) // Tikrina, ar norima detales gauti iš savo užsakymo ar ne
            {
                return RedirectToAction("MoreDetails", new { id = transport.TransportId });
            }
            return View(transport);
        }
        public async Task<IActionResult> MoreDetails(int? id)
        {
            var Transport = await _context.Transport
             .Include(o => o.Uzsakovas)
             .FirstOrDefaultAsync(m => m.TransportId == id);

            return View(Transport);
        }



        // GET: Transports/Create
        public IActionResult Create()
        {
            var model = new Transport();

            model.SunkvezimioTipas = "tipas";
            model.PakrovimoTipas = "pkrtipas";

            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber");
            return View(model);
        }

        // POST: Transports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransportId,UzsakovasId,FullFromLocation,LocationFromCountry,LocationFromDistrict,LocationFromCity,LocationFromStreet,LocationFronNumber,LocationFromZipCode,LocationFromLatitude,LocationFromLongitude,FullToLocation,LocationToCountry,LocationToDistrict,LocationToCity,LocationToStreet,LocationFromNumber,LocationToZipCode,LocationToLatitude,LocationToLongitude,GalimaNuoData,TransportoSkaicius,PakrovimoTipas,SunkvezimioTipas,ArGalimas,Svoris,Turis,Ilgis,PaleciuSk,Temperatura,PapildomaInfo,Kaina")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                transport.Uzsakovas = GautiAppUser();
                transport.UzsakovasId = transport.Uzsakovas.Id;
                transport.FullFromLocation = transport.GetFromLocation();
                _context.Add(transport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", transport.UzsakovasId);
            return View(transport);
        }

        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }
            if(transport.UzsakovasId == GautiAppUser().Id )
            {
                return View(transport);
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", transport.UzsakovasId);
            return RedirectToAction(nameof(Negalima));
        }

        // POST: Transports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransportId,UzsakovasId,FullFromLocation,LocationFromCountry,LocationFromDistrict,LocationFromCity,LocationFromStreet,LocationFronNumber,LocationFromZipCode,LocationFromLatitude,LocationFromLongitude,FullToLocation,LocationToCountry,LocationToDistrict,LocationToCity,LocationToStreet,LocationFromNumber,LocationToZipCode,LocationToLatitude,LocationToLongitude,GalimaNuoData,TransportoSkaicius,PakrovimoTipas,SunkvezimioTipas,ArGalimas,Svoris,Turis,Ilgis,PaleciuSk,Temperatura,PapildomaInfo,Kaina")] Transport transport)
        {
            if (id != transport.TransportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    transport.Uzsakovas = GautiAppUser();
                    transport.FullFromLocation = transport.GetFromLocation();
                    _context.Update(transport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transport.TransportId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", transport.UzsakovasId);
            return View(transport);
        }

        // GET: Transports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport
                .Include(t => t.Uzsakovas)
                .FirstOrDefaultAsync(m => m.TransportId == id);
            if (transport == null)
            {
                return NotFound();
            }

            if(transport.UzsakovasId == GautiAppUser().Id)
            {
                return View(transport);
            }
            return RedirectToAction(nameof(Negalima));

        }

        // POST: Transports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transport = await _context.Transport.FindAsync(id);
            _context.Transport.Remove(transport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _context.Transport.Any(e => e.TransportId == id);
        }
        public async Task<IActionResult> GetAllAsync() // Parodo tik prisijungusiu vartotojo pateiktus užsakymus
        {
            var IdUser = User.FindFirstValue(ClaimTypes.NameIdentifier);//gauname identityUserId
            //Gauname vieną Vartotojo id, kuris ir naudojasi applicaija. Jokių errorų. 
            int id = _context
                .AppUsers
                .Where(u => u.IdentityUserId == IdUser)
                .Select(u => u.Id)
                .SingleOrDefault();
            var Transport = _context.Transport.Where(m => m.UzsakovasId == id).ToListAsync();// reikia gauti vartotojo id ir tada vietoje vieno ideti        
            var data = Json(new { data = await Transport });
            return data;
        }
        public async Task<IActionResult> NaujasTokspat(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", transport.UzsakovasId);
            return View(transport);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NaujasTokspat(int id, [Bind("TransportId,UzsakovasId,FullFromLocation,LocationFromCountry,LocationFromDistrict,LocationFromCity,LocationFromStreet,LocationFronNumber,LocationFromZipCode,LocationFromLatitude,LocationFromLongitude,FullToLocation,LocationToCountry,LocationToDistrict,LocationToCity,LocationToStreet,LocationFromNumber,LocationToZipCode,LocationToLatitude,LocationToLongitude,GalimaNuoData,TransportoSkaicius,PakrovimoTipas,SunkvezimioTipas,ArGalimas,Svoris,Turis,Ilgis,PaleciuSk,Temperatura,PapildomaInfo,Kaina")] Transport transport)
        {
            if (id != transport.TransportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    transport.FullFromLocation = transport.GetFromLocation();
                    _context.Update(transport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transport.TransportId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", transport.UzsakovasId);
            return View(transport);
        }
        public async Task<IActionResult> AllTransports() //gražins visus užsakymus 
        {
            var order = _context.Transport.Include(i => i.Uzsakovas)
                .Select(t => new
                {
                    t.GalimaNuoData,
                    t.FullFromLocation,
                    t.PakrovimoTipas,
                    t.SunkvezimioTipas,
                    t.TransportoSkaicius,
                    t.PaleciuSk,
                    t.Turis,
                    t.Svoris,
                    t.Temperatura,
                    t.Ilgis,
                    t.Kaina,
                    t.TransportId,
                    t.Uzsakovas,
                    t.PapildomaInfo,


                })
                .ToListAsync();
            var data = Json(new { data = await order });
            return data;
        }
        public IActionResult All()
        {

            return View();
        }
        public int GautiAppUserId()
        {
            var IdUser = User.FindFirstValue(ClaimTypes.NameIdentifier);//gauname identityUserId
            //Gauname vieną Vartotojo id, kuris ir naudojasi applicaija. Jokių errorų. 
            int id = _context
                .AppUsers
                .Where(u => u.IdentityUserId == IdUser)
                .Select(u => u.Id)
                .SingleOrDefault();
            return id;

        }
        public AppUser GautiAppUser()
        {
            var IdUser = User.FindFirstValue(ClaimTypes.NameIdentifier);//gauname identityUserId
            var AppUser = _context.AppUsers.Where(u => u.IdentityUserId == IdUser).FirstOrDefault();
            return AppUser;

        }
        public IActionResult Negalima()
        {
            return View();
        }

    }
}
