using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models;

namespace Storage.Controllers
{
    public class CategoryDbsController : Controller
    {
        private readonly StorageContext _context;

        public CategoryDbsController(StorageContext context)
        {
            _context = context;
        }

        // GET: CategoryDbs
        public async Task<IActionResult> Index()
        {
              return _context.CategoryDb != null ? 
                          View(await _context.CategoryDb.ToListAsync()) :
                          Problem("Entity set 'StorageContext.CategoryDb'  is null.");
        }

        // GET: CategoryDbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryDb == null)
            {
                return NotFound();
            }

            var categoryDb = await _context.CategoryDb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryDb == null)
            {
                return NotFound();
            }

            return View(categoryDb);
        }

        // GET: CategoryDbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryDbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryDb categoryDb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDb);
        }

        // GET: CategoryDbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryDb == null)
            {
                return NotFound();
            }

            var categoryDb = await _context.CategoryDb.FindAsync(id);
            if (categoryDb == null)
            {
                return NotFound();
            }
            return View(categoryDb);
        }

        // POST: CategoryDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryDb categoryDb)
        {
            if (id != categoryDb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryDbExists(categoryDb.Id))
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
            return View(categoryDb);
        }

        // GET: CategoryDbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryDb == null)
            {
                return NotFound();
            }

            var categoryDb = await _context.CategoryDb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryDb == null)
            {
                return NotFound();
            }

            return View(categoryDb);
        }

        // POST: CategoryDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryDb == null)
            {
                return Problem("Entity set 'StorageContext.CategoryDb'  is null.");
            }
            var categoryDb = await _context.CategoryDb.FindAsync(id);
            if (categoryDb != null)
            {
                _context.CategoryDb.Remove(categoryDb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDbExists(int id)
        {
          return (_context.CategoryDb?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
