using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoveAround.Data;
using MoveAround.Models;

namespace MoveAround.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class AddToRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public AppUser AppUser { get; set; }

        public AddToRolesController(ApplicationDbContext context,  UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: AddToRoles
        public IActionResult Index()
        {
            return View();
        }

        // GET: AddToRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AddToRoles/Create
        public IActionResult Create()
        {
            

            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", appUser.IdentityUserId);
            return View(appUser);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,AddressCity," +
            " AddressGatveNr,BuisnessName,BuisnessCode" +
            "Balance,IdentityUserId,Tipas,PVM,BuisnesAdressCity,BuisnesAdressStreet," +
            ",EstablishedDate,PhoneNumber,BuisnessEmail, BuisnessCode, FirstName, LastName, IdentityUserId")] AppUser appUser)
        {
            if (id != AppUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(AppUser.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", AppUser.IdentityUserId);
            return View(AppUser);
        }    

        private bool AppUserExists(int id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {          
            var zmones = _context.AppUsers.Include(u => u.User).ToListAsync();
            var data = Json(new { data = await zmones });
            return data;
        }

        //
        [HttpGet]
        public IActionResult AddMore(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var IsKonteksto = _context.AppUsers.Where(u => u.Id == id.Value).Include(u => u.User).FirstOrDefault();                
                AppUser = IsKonteksto;
                var Roles = _userManager.GetRolesAsync(AppUser.User).Result.ToList();
                AppUser.AllUserROles = Roles;
                return View(AppUser);
            }
        }
        [HttpPost]        
        public async Task<IActionResult> AddMore()
        {
            var role = AppUser.Tipas;
            var IsKonteksto = _context.AppUsers.Where(u => u.Id == AppUser.Id).Include(u=> u.User).FirstOrDefault();
            AppUser = IsKonteksto;
            var x = await _userManager.IsInRoleAsync(AppUser.User, role) ;
           
            if (!(x))
            {
                //List<string>  Roles = _userManager.GetRolesAsync(AppUser.User).Result.ToList();
                await _userManager.AddToRoleAsync(AppUser.User, role);
                await _context.SaveChangesAsync();

                ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", AppUser.IdentityUserId);
                return RedirectToAction("AddMore", "AddToRoles", new { @id = AppUser.Id });

            }
            else
            {
                return RedirectToAction("Index", "AddToRoles");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var role = AppUser.Tipas;
            var IsKonteksto = _context.AppUsers.Where(u => u.Id == AppUser.Id).Include(u => u.User).FirstOrDefault();
            AppUser = IsKonteksto;            
            var x = await _userManager.IsInRoleAsync(AppUser.User, role);
            if (x)
            {                           
                await _userManager.RemoveFromRoleAsync(IsKonteksto.User, role);//Isima is user i roles
                await _context.SaveChangesAsync();
                return RedirectToAction("AddMore", "AddToRoles", new { @id = AppUser.Id });
            }
            else
            {
                return RedirectToAction("Index", "AddToRoles");
            }
        }


        public async Task<AppUser> GetUpdatedusersAsync(int _id)
        {
            var appUser = await _context.AppUsers.FindAsync(_id); //Gauname AppUSer pvz Mantas Ivanauskas
            var IdUser = await _userManager.FindByIdAsync(appUser.IdentityUserId); //Gauname IDentiyUser (nes kažkodėl neužbidina IškRO KAIP TURĖTU BŪTI PVZ. appUser.User                
            appUser.User = IdUser; //tada saku, kad AplikacijosUSerio. Identity useris yra kątik gautas usersi
            _context.Update(appUser);
            _context.SaveChanges();
            //updateinam išsaugom
            var Roles = _userManager.GetRolesAsync(IdUser).Result.ToList();// Gauname visas Identiy userio reolsKonvertuojme i ilista
            appUser.AllUserROles = Roles; //sakome tada turime visas roles
            _context.Update(appUser);
            _context.SaveChanges();
            return appUser;
        }

        public async Task<List<string>> GetListoFRoles(int _id)
        {
            var appUser = await _context.AppUsers.FindAsync(_id); 
            var IdUser = await _userManager.FindByIdAsync(appUser.IdentityUserId);
            var Roles = _userManager.GetRolesAsync(IdUser).Result.ToList();
            return Roles;
        }
        public AppUser GautiAppUser()
        {
            var IdUser = User.FindFirstValue(ClaimTypes.NameIdentifier);//gauname identityUserId
            var AppUser = _context.AppUsers.Where(u => u.IdentityUserId == IdUser).FirstOrDefault();
            return AppUser;

        }
    }
}
