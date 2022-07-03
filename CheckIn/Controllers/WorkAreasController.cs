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

        // GET: WorkAreas/Details/5
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

        // GET: WorkAreas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkAreas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] WorkArea workArea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workArea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workArea);
        }

        // GET: WorkAreas/Edit/5
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

        // POST: WorkAreas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WorkArea workArea)
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

        // GET: WorkAreas/Delete/5
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

        // POST: WorkAreas/Delete/5
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
