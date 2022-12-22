using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models;

namespace Storage.Controllers
{
    public class ProductViewModelsController : Controller
    {
        private readonly StorageContext _context;

        public ProductViewModelsController(StorageContext context)
        {
            _context = context;
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

        // GET: ProductViewModels
        public async Task<IActionResult> Index()
        {
            return _context.ProductViewModel != null ?
                        View(await _context.ProductViewModel.ToListAsync()) :
                        Problem("Entity set 'StorageContext.ProductViewModel'  is null.");
        }

        // GET: ProductViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: ProductViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Count,InventoryValue")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Count,InventoryValue")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewModelExists(productViewModel.Id))
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
            return View(productViewModel);
        }

        // GET: ProductViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: ProductViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductViewModel == null)
            {
                return Problem("Entity set 'StorageContext.ProductViewModel'  is null.");
            }
            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            if (productViewModel != null)
            {
                _context.ProductViewModel.Remove(productViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewModelExists(int id)
        {
            return (_context.ProductViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
