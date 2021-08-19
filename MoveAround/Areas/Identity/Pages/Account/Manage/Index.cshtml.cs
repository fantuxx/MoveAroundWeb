using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MoveAround.Data;
using MoveAround.Models;
using SQLitePCL;

namespace MoveAround.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
       
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            
            UserManager<IdentityUser> userManager,
             ApplicationDbContext context,
            SignInManager<IdentityUser> signInManager)
        {
            
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
      [BindProperty ]
        
        public int id { get; set; }
        [Display(Name = "Vartotojo Vardas")]
        public string Tipas { get; set; }

        [Display(Name ="Jūsų rolės:")]
        public List<string> UserRoles { get; set; }
        [Display(Name = "Vartotojo Vardas:")]
        public string Username { get; set; }

        [BindProperty]
        public AppUser AppUser { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Telefono numeris")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            List<string> roles = _userManager.GetRolesAsync(user).Result.ToList<string>();
            UserRoles = roles;
            Username = userName;          
            Tipas = "Padar";
           
           
                var _appUser = _context.AppUsers.Where(u => u.IdentityUserId == user.Id).FirstOrDefault();
                AppUser = _appUser;
            
           
           
            /*


               string id = idUSer.Id.ToString();
               List<AppUser> appUsers = await _context.AppUsers.ToListAsync<AppUser>();
               dynamic mymodel = new ExpandoObject();            
                AppUser vartotojas = appUsers.Find (x => x.IdentityUserId == id);
               */

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
         

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
           
             

            await LoadAsync(user);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync([Bind("Id,AccountNumber,AddressCity," +
            " AddressGatveNr,BuisnessName,BuisnessCode" +
            "Balance,IdentityUserId,Tipas,PVM,BuisnesAdressCity,BuisnesAdressStreet," +
            ",EstablishedDate,PhoneNumber,BuisnessEmail, BuisnessCode, FirstName, LastName")] AppUser appUser)
        {
            var user = await _userManager.GetUserAsync(User);
            var _appUser = _context.AppUsers.Where(u => u.IdentityUserId == user.Id).FirstOrDefault();
            //cia nurodome, kas leidziama keisti varotojui
            _appUser.AddressCity = appUser.AddressCity;
            _appUser.PhoneNumber = appUser.PhoneNumber;
            _appUser.AccountNumber = appUser.AccountNumber;
          //  _appUser.BuisnessName = appUser.BuisnessName;
            _appUser.BuisnesAdressCity = appUser.BuisnesAdressCity;
            _appUser.BuisnesAdressStreet = appUser.BuisnesAdressStreet;
            _appUser.BuisnessCode = appUser.BuisnessCode;
            //_appUser.BuisnessEmail = appUser.BuisnessEmail;
           
           
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            else
            {
            }

            if (!ModelState.IsValid)
            {
                    await LoadAsync(user);
                    _context.Update(_appUser);
                    await _context.SaveChangesAsync();
                    return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
