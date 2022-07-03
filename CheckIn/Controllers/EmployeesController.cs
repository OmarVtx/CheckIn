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
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext _context;

        public EmployeesController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Employees != null ?
                        View(await _context.Employees
                        .Include(e => e.EmployeeType)
                        .Include(e => e.Gender)
                        .Include(e => e.Warehouse)
                        .Include(e => e.WorkArea)
                        .ToListAsync()) :
                        Problem("Entity set 'DatabaseContext.Employees'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.EmployeeType)
                .Include(e => e.Gender)
                .Include(e => e.Warehouse)
                .Include(e => e.WorkArea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            
            return View(employee);
        }

        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Name");
            ViewData["EmployeeTypeId"] = new SelectList(_context.EmployeeType, "Id", "Name");
            ViewData["WorkAreaId"] = new SelectList(_context.WorkArea, "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Name");
            ViewData["EmployeeTypeId"] = new SelectList(_context.EmployeeType, "Id", "Name");
            ViewData["WorkAreaId"] = new SelectList(_context.WorkArea, "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "Id", "Name");
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Name");
            ViewData["EmployeeTypeId"] = new SelectList(_context.EmployeeType, "Id", "Name");
            ViewData["WorkAreaId"] = new SelectList(_context.WorkArea, "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "Id", "Name");
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.EmployeeType)
                .Include(e => e.Gender)
                .Include(e => e.Warehouse)
                .Include(e => e.WorkArea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'DatabaseContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
