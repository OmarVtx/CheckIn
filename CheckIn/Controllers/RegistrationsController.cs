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
    public class RegistrationsController : Controller
    {
        private readonly DatabaseContext _context;

        public RegistrationsController(DatabaseContext context)
        {
            _context = context;
        }

        //// GET: Registrations
      
        public async Task<IActionResult> Index(DateTime? startDate = null, DateTime? endDate = null)
        {
            
            var records = from r in _context.Records.Include(e => e.Employee)
                       select r;

            if (startDate != null && endDate != null)
            {
                endDate = endDate.Value.AddDays(1);
                records = records.Where(d => d.Entry >= startDate && d.Exit <= endDate);
                return View(await records.ToListAsync());
            }

            return View(await records.ToListAsync());
        }

        public IActionResult Input()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Input(Registration registration)
        {
            if (ModelState.IsValid)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Id == registration.EmployeeId);
                if (employee != null)
                {
                    var record = _context.Records.FirstOrDefault(r => r.EmployeeId == registration.EmployeeId && r.Exit == null);
                    if (record != null)
                    {
                        record.Exit = DateTime.Now;
                        _context.Update(record);
                    }
                    else
                    {
                        var newRegistration = new Registration();
                        newRegistration.Entry = DateTime.Now;
                        newRegistration.EmployeeId = employee.Id;
                        _context.Add(newRegistration);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            } 
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var registration = await _context.Records
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Records == null)
            {
                return Problem("Entity set 'DatabaseContext.Employees'  is null.");
            }
            var registration = await _context.Records.FindAsync(id);
            if (registration != null)
            {
                _context.Records.Remove(registration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return (_context.Records?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
