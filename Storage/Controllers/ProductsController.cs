﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageContext _context;

        public ProductsController(StorageContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DropDown(ProductViewModel view)
        {
            var products = await _context.Product.Where(p=>p.Category==view.Category).ToListAsync();
            //var cats = products.Select(p => p.Category).Distinct().ToList();
            var cats = await _context.Product.Select(p => p.Category).Distinct().ToListAsync();

            ProductViewModel newView = new()
            {
                Products= products,
                Categories = cats.Select(c=>new SelectListItem { Text=c.ToString() }).ToList(),
            };
            return View(newView);
        }

        public async Task<IActionResult> Filter(string filterStr)
        {

            var productList = string.IsNullOrWhiteSpace(filterStr) ? await _context.Product.ToListAsync() : await _context.Product.Where(p => p.Name.Contains(filterStr)).ToListAsync();

            var categories = await _context.Product.Select(p => p.Category).Distinct().ToListAsync();

            var newView = await _context.Product.Select(e => new ProductViewModel
            {
                Name = e.Name,
                Price = e.Price,
                Count = e.Count,
                InventoryValue = e.Count * e.Price,
                Categories = categories.Select(c => new SelectListItem { Text = c.ToString() }).ToList(),
                Products = productList,
            }).ToListAsync();

            return View("Filter", newView);
        }

        public async Task<IActionResult> Storage(ProductViewModel view)
        {

            var products = view==null ? await _context.Product.ToListAsync() : await _context.Product.Where(p => p.Category == view.Category).ToListAsync();
            //var cats = products.Select(p => p.Category).Distinct().ToList();
            var cats = await _context.Product.Select(p => p.Category).Distinct().ToListAsync();

            IEnumerable<ProductViewModel> productViewModels = null;

            ProductViewModel newView = new()
            {
                Products = products,
                Categories = cats.Select(c => new SelectListItem { Text = c.ToString() }).ToList(),
            };
            return View(newView);
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            return _context.Product != null ?
                        View(await _context.Product.ToListAsync()) :
                        Problem("Entity set 'StorageContext.Product'  is null.");
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,OrderDate,Category,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,OrderDate,Category,Shelf,Count,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'StorageContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
