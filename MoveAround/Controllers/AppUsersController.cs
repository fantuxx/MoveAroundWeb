using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoveAround.Data;
using MoveAround.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace MoveAround.Controllers
{
    [Authorize]
    public class AppUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AppUsersController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
  

        // GET: AppUsers
        public IActionResult Index([Bind("Id,AccountNumber,AddressCity," +
            " AddressGatveNr,BuisnessName,BuisnessCode" +
            "Balance,IdentityUserId,Tipas,PVM,BuisnesAdressCity,BuisnesAdressStreet," +
            "Balance,IdentityUserId,Tipas,PVM,BuisnesAdressCity,BuisnesAdressStreet," +
            ",EstablishedDate,PhoneNumber,BuisnessEmail, BuisnessCode, FirstName, LastName")]
        AppUser appuser)
        {
            var applicationDbContext = _context.AppUsers.Include(c => c.User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //duos usersID               
            //pridedame acc i pasirinkta role
            IdentityUser user = _userManager.FindByIdAsync(userId).Result;//gauname user  
            return View();
        }



        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appuser == null)
            {
                return NotFound();
            }

            return View(appuser);
        }

        [Authorize(Roles = "Users")]
        // GET: AppUsers/Create
        public IActionResult Create()
        {
            var model = new AppUser();
            model.Tipas = "U";

            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View(model);
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Users")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNumber,AddressCity," +
            " AddressGatveNr,BuisnessName,BuisnessCode" +
            "Balance,IdentityUserId,Tipas,PVM,BuisnesAdressCity,BuisnesAdressStreet," +
            ",EstablishedDate,PhoneNumber,BuisnessEmail, BuisnessCode, FirstName, LastName")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //duos usersID  
                var user = _userManager.FindByIdAsync(userId).Result;//gauname user      
                appUser.IdentityUserId = userId;
               
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                appUser.SetEmail();
                appUser.IsConfirmed = true;
                _context.Update(appUser);
                await _context.SaveChangesAsync();
                //pridedame acc i pasirinkta role

                await _userManager.AddToRoleAsync(user, appUser.Tipas);
                await _context.SaveChangesAsync();
                await _userManager.RemoveFromRoleAsync(user, "Users");//Isima is user i roles
                await _context.SaveChangesAsync();                
                ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", appUser.IdentityUserId);
                return RedirectToAction("Index");

            }
            else
            {
                return View(appUser);
            }



        }

        // GET: AppUsers/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _context.AppUsers.FindAsync(id);
            if (appuser == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", appuser.IdentityUserId);
            return View(appuser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,AddressCity," +
            " AddressGatveNr,BuisnessName,BuisnessCode" +
            "Balance,IdentityUserId,Tipas,PVM,BuisnesAdressCity,BuisnesAdressStreet," +
            ",EstablishedDate,PhoneNumber,BuisnessEmail, BuisnessCode, FirstName, LastName")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", appUser.IdentityUserId);
            return View(appUser);
        }

        // GET: AppUsers/Delete/5


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUser = await _context.AppUsers.FindAsync(id);
            _context.AppUsers.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }
        [Authorize(Roles ="Vezejas")]
        public IActionResult Vezejas()
        {
            return View();
        }

        [Authorize(Roles = "Uzsakovas")]
        public IActionResult Uzsakovas()
        {
            return View();
        }
        [Authorize(Roles = "Users")]
        public IActionResult Users()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

      


    }
}
