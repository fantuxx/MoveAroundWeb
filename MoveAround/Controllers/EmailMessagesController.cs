using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoveAround.Data;
using MoveAround.Models;
using System.Net.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace MoveAround.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class EmailMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public EmailMessagesController(ApplicationDbContext context,  IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _context = context;
        }

        // GET: EmailMessages
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailMessage.ToListAsync());
        }
       

        // GET: EmailMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailMessage = await _context.EmailMessage
                .FirstOrDefaultAsync(m => m.id == id);
            if (emailMessage == null)
            {
                return NotFound();
            }

            return View(emailMessage);
        }

        // GET: EmailMessages/CreateEma
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("id,Name,Email,Subject,Message, DateCreated")] EmailMessage emailMessage)
        {
            if (ModelState.IsValid)
            {
                emailMessage.DateCreated = DateTime.Now;
                _context.Add(emailMessage);
                await _context.SaveChangesAsync();
                await _emailSender.SendEmailAsync("man.ivanauskas@gmail.com", "Sistemos Žinutė " ,//vietoje man.ivanauskas.com rasome benda el pasta kuria administruoja adminsa
                      "Gauta Nauja Žinutė nuo: "+ emailMessage.Email+"<br/>" +
                      "<br/>" +
                      emailMessage.Name +" <strong> Žinutės tekstas: </strong> <br/>" +
                      emailMessage.Message);
                



                return RedirectToAction("ThankYou", "EmailMessages");
            }
            return View(emailMessage);
        }

        // GET: EmailMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailMessage = await _context.EmailMessage.FindAsync(id);
            if (emailMessage == null)
            {
                return NotFound();
            }
            return View(emailMessage);
        }

        // POST: EmailMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Email,Subject,Message")] EmailMessage emailMessage)
        {
            if (id != emailMessage.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailMessageExists(emailMessage.id))
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
            return View(emailMessage);
        }

        // GET: EmailMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailMessage = await _context.EmailMessage
                .FirstOrDefaultAsync(m => m.id == id);
            if (emailMessage == null)
            {
                return NotFound();
            }

            return View(emailMessage);
        }

        // POST: EmailMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailMessage = await _context.EmailMessage.FindAsync(id);
            _context.EmailMessage.Remove(emailMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailMessageExists(int id)
        {
            return _context.EmailMessage.Any(e => e.id == id);
        }
        [AllowAnonymous]
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
