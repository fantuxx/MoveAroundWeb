using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoveAround.Data;
using MoveAround.Services;

namespace MoveAround.Controllers
{
    public class GoogleAdresAPIsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoogleAdresAPIsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GoogleAdresAPIs
        public async Task<IActionResult> Index()
        {
            return View(await _context.GoogleAdresAPI.ToListAsync());
        }

        // GET: GoogleAdresAPIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var googleAdresAPI = await _context.GoogleAdresAPI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (googleAdresAPI == null)
            {
                return NotFound();
            }

            return View(googleAdresAPI);
        }

        // GET: GoogleAdresAPIs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GoogleAdresAPIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address")] GoogleAdresAPI googleAdresAPI)
        {
            if (ModelState.IsValid)
            {
                _context.Add(googleAdresAPI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(googleAdresAPI);
        }

        // GET: GoogleAdresAPIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var googleAdresAPI = await _context.GoogleAdresAPI.FindAsync(id);
            if (googleAdresAPI == null)
            {
                return NotFound();
            }
            return View(googleAdresAPI);
        }

        // POST: GoogleAdresAPIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address")] GoogleAdresAPI googleAdresAPI)
        {
            if (id != googleAdresAPI.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(googleAdresAPI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoogleAdresAPIExists(googleAdresAPI.Id))
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
            return View(googleAdresAPI);
        }

        // GET: GoogleAdresAPIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var googleAdresAPI = await _context.GoogleAdresAPI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (googleAdresAPI == null)
            {
                return NotFound();
            }

            return View(googleAdresAPI);
        }

        // POST: GoogleAdresAPIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var googleAdresAPI = await _context.GoogleAdresAPI.FindAsync(id);
            _context.GoogleAdresAPI.Remove(googleAdresAPI);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoogleAdresAPIExists(int id)
        {
            return _context.GoogleAdresAPI.Any(e => e.Id == id);
        }
    }
}
