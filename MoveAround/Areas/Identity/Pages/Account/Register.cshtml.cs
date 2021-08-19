using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace MoveAround.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;

        }
        [BindProperty]
        public InputModel Input { get; set; }

       
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "Netinkamas elektronino pašto formatas")]
            [Display(Name = "Elektoninio pašto adresas")]
            public string Email { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = " {0} Negali būti trumpsnis nei {2} ir ilgesnis nei {1} ženklai.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Slaptažodis")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Pakartokite slaptažodį")]
            [Compare("Password", ErrorMessage = "Slaptažodžiai nestutampa.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            //Sukuriame roles
            bool x = await _roleManager.RoleExistsAsync("Moderator");
            bool y = await _roleManager.RoleExistsAsync("SuperAdmin");
            
            if (!( y && x))//jeigu nera to arba to
            {
                var role = new IdentityRole();
                var role1 = new IdentityRole();
                var role2 = new IdentityRole();
                var role3 = new IdentityRole();
                var role4 = new IdentityRole();
                var role5 = new IdentityRole();
                role.Name = "Users";
                role1.Name = "Vezejas";
                role2.Name = "Uzsakovas";
                role3.Name = "Admin";
                role4.Name = "Moderator";
                role5.Name = "SuperAdmin";
                await _roleManager.CreateAsync(role);
                await _roleManager.CreateAsync(role1);
                await _roleManager.CreateAsync(role2);
                await _roleManager.CreateAsync(role3);
                await _roleManager.CreateAsync(role4);
                await _roleManager.CreateAsync(role5);
            }
            else
            {
            }
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {                    
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    if (user.UserName == "man.ivanauskas@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(user, "SuperAdmin");
                    }

                    await _emailSender.SendEmailAsync(Input.Email, "Patvirtinkite e-mail adresą",
                        $"Norėdami patvirtini savo e-pašto adresą <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Spauskite čia</a>. <br /> Pagarbiai,<br />Jūsų Move-Around Komanda");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _userManager.AddToRoleAsync(user, "Users");//prideda i roles

                        return RedirectToAction("Index", "AppUsers");//sita eilute padeta tyca nukreipia i nauja forma pi registarcijos
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
