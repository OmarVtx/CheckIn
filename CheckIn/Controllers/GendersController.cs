using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckIn.Models;

namespace CheckIn.Controllers
{
    public class GendersController : Controller
    {
        private readonly DatabaseContext _context;

        public GendersController(DatabaseContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
              return _context.Gender != null ? 
                          View(await _context.Gender.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.Gender'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gender gender)
        {
            if (id != gender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.Id))
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
            return View(gender);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gender == null)
            {
                return Problem("Entity set 'DatabaseContext.Gender'  is null.");
            }
            var gender = await _context.Gender.FindAsync(id);
            if (gender != null)
            {
                _context.Gender.Remove(gender);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenderExists(int id)
        {
          return (_context.Gender?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
