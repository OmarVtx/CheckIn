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
    public class EmployeeTypesController : Controller
    {
        private readonly DatabaseContext _context;

        public EmployeeTypesController(DatabaseContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
              return _context.EmployeeType != null ? 
                          View(await _context.EmployeeType.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.EmployeeType'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeType == null)
            {
                return NotFound();
            }

            var employeeType = await _context.EmployeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeType == null)
            {
                return NotFound();
            }

            return View(employeeType);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeType);
        }

        /
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeType == null)
            {
                return NotFound();
            }

            var employeeType = await _context.EmployeeType.FindAsync(id);
            if (employeeType == null)
            {
                return NotFound();
            }
            return View(employeeType);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EmployeeType employeeType)
        {
            if (id != employeeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTypeExists(employeeType.Id))
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
            return View(employeeType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeType == null)
            {
                return NotFound();
            }

            var employeeType = await _context.EmployeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeType == null)
            {
                return NotFound();
            }

            return View(employeeType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeType == null)
            {
                return Problem("Entity set 'DatabaseContext.EmployeeType'  is null.");
            }
            var employeeType = await _context.EmployeeType.FindAsync(id);
            if (employeeType != null)
            {
                _context.EmployeeType.Remove(employeeType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeTypeExists(int id)
        {
          return (_context.EmployeeType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
