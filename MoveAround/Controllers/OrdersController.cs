using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoveAround.Data;
using MoveAround.Models;
using MoveAround.Services;

namespace MoveAround.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
      
      
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Orders
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult All()
        {

            return View();
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Uzsakovas)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            int orderid = order.OrderId;
            if (order == null)
            {
                return NotFound();
            }
            if (order.UzsakovasId != GautiAppUser().Id)
            {
                return RedirectToAction("MoreDetails", new { id = order.OrderId });
            }
                return View(order);

        }
        public async Task<IActionResult> MoreDetailsAsync(int? id)
        {
            var order = await _context.Order
             .Include(o => o.Uzsakovas)
             .FirstOrDefaultAsync(m => m.OrderId == id);

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "Id");
            Order model = new Order();
            model.Tipas = "asd";
            model.PakrovimoTipas = "asd";

            return View(model);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId, Tipas,PakrovimoTipas,Svoris,Turis,Ilgis,PaleciuSk," +
            "Temperatura,PapildomaInfo,Kaina,UzsakovasId,FromStreet, ToStreet,FromDistrict, " +
            "ToHouseNo,FromCity, ToCity, FromHouseNo,ToHouseNo, ToDistrict ,FromZipCode, ToZipCode, FromCountry, ToCountry, " +
            "FromLatitude, FromLongtitude, ToLatitude, ToLongtitude, TDate,  FDate,  ")] Order order)
        {            
            var validator = new OrderValidator();
            var results = validator.Validate(order);
            results.AddToModelState(ModelState, null);           
            if (ModelState.IsValid)
            {

                order.Uzsakovas = GautiAppUser();
                order.UzsakovasId = order.Uzsakovas.Id;               
              
                order.FromAddress = order.GetFullFromAddress();
                order.ToAddress = order.GetFullToAddress();
                order.TDate.Value.ToString("d");
                order.FDate.Value.ToString("d");
                _context.Add(order);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", order.UzsakovasId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            if ( order.UzsakovasId == GautiAppUser().Id)//Tikrina, ar tas pats žmogus kuris pateikė užsakymą bando jį keisti?
            {
                return View(order);
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", order.UzsakovasId);
            return RedirectToAction(nameof(Negalima));

        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Tipas,PakrovimoTipas,Svoris,Turis,Ilgis,PaleciuSk," +
            "Temperatura,PapildomaInfo,Kaina,UzsakovasId, Uzsakovas, FromStreet, ToStreet,FromDistrict, " +
            "ToHouseNo,FromCity, ToCity, FromHouseNo,ToHouseNo, ToDistrict ,FromZipCode, ToZipCode, FromCountry, ToCountry, " +
            "FromLatitude, FromLongtitude, ToLatitude, ToLongtitude, TDate,  FDate ")] Order order)
        {
          
            if (id != order.OrderId )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                order.Uzsakovas = GautiAppUser();
                order.FromAddress = order.GetFullFromAddress();
                order.ToAddress = order.GetFullToAddress();
                order.TDate.Value.ToString("d");
                order.FDate.Value.ToString("d");
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Uzsakovas)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            //tikriname ar taspats asmuo ir nori trinti kuris pateikė užsakymą
            if (order.UzsakovasId == GautiAppUser().Id)
            {
                return View(order);
            }
            //jeigu jau niekas nesuveikė, reiškiasi negalima trinti
            return RedirectToAction(nameof(Negalima));


        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
       
        [HttpGet]        
        public async Task<IActionResult> MyOrders() // Parodo tik prisijungusiu vartotojo pateiktus užsakymus
        { 
            var IdUser = User.FindFirstValue(ClaimTypes.NameIdentifier);//gauname identityUserId
            //Gauname vieną Vartotojo id, kuris ir naudojasi applicaija. Jokių errorų. 
            int id = _context 
                .AppUsers
                .Where(u => u.IdentityUserId == IdUser)
                .Select(u => u.Id)
                .SingleOrDefault();          
            
            var order = _context.Order.Where(m=> m.UzsakovasId == id).ToListAsync();// reikia gauti vartotojo id ir tada vietoje vieno ideti        
            var data = Json(new { data =  await order });
            return data;
        }
        public async Task<IActionResult> NewAlike(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", order.UzsakovasId);
            
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAlike(int id, [Bind("OrderId,Tipas,PakrovimoTipas,Svoris,Turis,Ilgis,PaleciuSk," +
            "Temperatura,PapildomaInfo,Kaina,UzsakovasId, Uzsakovas, FromStreet, ToStreet,FromDistrict, " +
            "ToHouseNo,FromCity, ToCity, FromHouseNo,ToHouseNo, ToDistrict ,FromZipCode, ToZipCode, FromCountry, ToCountry, " +
            "FromLatitude, FromLongtitude, ToLatitude, ToLongtitude, TDate,  FDate ")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.FromAddress = order.GetFullFromAddress();
                    order.ToAddress = order.GetFullToAddress();
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["UzsakovasId"] = new SelectList(_context.AppUsers, "Id", "AccountNumber", order.UzsakovasId);
            return View(order);
        }
        public IActionResult Negalima()
        {
            return View();
        }

        public async Task<IActionResult> AllOrdersAsync() //gražins visus užsakymus 
            {     
            var order = _context.Order.Include(i => i.Uzsakovas)
                .Select(t => new
            {
               t.OrderId,
               t.Uzsakovas,
               t.UzsakovasId,
               t.FromAddress,
               t.FDate,
               t.TDate,
               t.ToAddress,
               t.Tipas,
               t.PakrovimoTipas,
               t.Svoris,
               t.PaleciuSk,
               t.Ilgis,
               t.Temperatura,
               t.Kaina,
               t.PapildomaInfo,
               t.Turis
            })                
                .ToListAsync();  
            var data = Json(new { data = await order });
            return data;
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
    }
   

}
