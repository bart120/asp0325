using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocaSub.Data;
using DocaSub.Models;

namespace DocaSub.Areas.Backoffice.Controllers
{
    [Area("Backoffice")]
    public class SubventionsController : Controller
    {
        private readonly DocaDbContext _context;

        public SubventionsController(DocaDbContext context)
        {
            _context = context;
        }

        // GET: Backoffice/Subventions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subventions.ToListAsync());
        }

        // GET: Backoffice/Subventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subvention = await _context.Subventions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subvention == null)
            {
                return NotFound();
            }

            return View(subvention);
        }

        // GET: Backoffice/Subventions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Backoffice/Subventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Partner,Category,Start,End")] Subvention subvention)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subvention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subvention);
        }

        // GET: Backoffice/Subventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subvention = await _context.Subventions.FindAsync(id);
            if (subvention == null)
            {
                return NotFound();
            }
            return View(subvention);
        }

        // POST: Backoffice/Subventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Partner,Category,Start,End")] Subvention subvention)
        {
            if (id != subvention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subvention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubventionExists(subvention.Id))
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
            return View(subvention);
        }

        // GET: Backoffice/Subventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subvention = await _context.Subventions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subvention == null)
            {
                return NotFound();
            }

            return View(subvention);
        }

        // POST: Backoffice/Subventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subvention = await _context.Subventions.FindAsync(id);
            if (subvention != null)
            {
                _context.Subventions.Remove(subvention);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubventionExists(int id)
        {
            return _context.Subventions.Any(e => e.Id == id);
        }
    }
}
