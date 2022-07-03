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
    public class WorkAreasController : Controller
    {
        private readonly DatabaseContext _context;

        public WorkAreasController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: WorkAreas
        public async Task<IActionResult> Index()
        {
              return _context.WorkArea != null ? 
                          View(await _context.WorkArea.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.WorkArea'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkArea == null)
            {
                return NotFound();
            }

            var workArea = await _context.WorkArea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workArea == null)
            {
                return NotFound();
            }

            return View(workArea);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkArea workArea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workArea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workArea);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkArea == null)
            {
                return NotFound();
            }

            var workArea = await _context.WorkArea.FindAsync(id);
            if (workArea == null)
            {
                return NotFound();
            }
            return View(workArea);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkArea workArea)
        {
            if (id != workArea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workArea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkAreaExists(workArea.Id))
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
            return View(workArea);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkArea == null)
            {
                return NotFound();
            }

            var workArea = await _context.WorkArea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workArea == null)
            {
                return NotFound();
            }

            return View(workArea);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkArea == null)
            {
                return Problem("Entity set 'DatabaseContext.WorkArea'  is null.");
            }
            var workArea = await _context.WorkArea.FindAsync(id);
            if (workArea != null)
            {
                _context.WorkArea.Remove(workArea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkAreaExists(int id)
        {
          return (_context.WorkArea?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
