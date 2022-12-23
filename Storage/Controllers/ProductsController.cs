using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageContext _context;

        public ProductsController(StorageContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search(ProductViewModel view)
        {
            IQueryable<Product> query = _context.Product.Include(p => p.CategoryDb);
            //var query2 = _context.Product.Select(p => new
            //{
            //    SomeProp = p.CategoryDb.Name
            //});

            if (!string.IsNullOrWhiteSpace(view.Name))
            {
                query = query.Where(p => p.Name.Contains(view.Name));
            }

            if (view.CategoryId != null)
            {
                query = query.Where(p => p.CategoryId == view.CategoryId);
            }
            var products = await query.ToListAsync();

            //var query = string.IsNullOrWhiteSpace(view.Name) ?
            //    _context.Product.Include(c => c.CategoryDb) :
            //    _context.Product.Include(c => c.CategoryDb).Where(p => p.Name.Contains(view.Name));

            //query = view.CategoryId is null ?
            //    query :
            //    query.Where(p => p.CategoryId == view.CategoryId);

            //var products = await query.ToListAsync();

            //get all categories, not just those in filtered view
            //from database
            var categoriesDb = await _context.Product.Include(c => c.CategoryDb).Select(p => p.CategoryDb).Distinct().ToListAsync();

            ProductViewModel productViewModel = new()
            {
                Products = products,
                //Categories = categories.Select(c=>new SelectListItem { Text=c.ToString() }).ToList(),
                CategoriesDb = categoriesDb
                .Select(c => new SelectListItem { Text = c.Name.ToString(), Value = c.Id.ToString() })
                .ToList(),
            };
            return View(productViewModel);
        }

        public async Task<IActionResult> Storage()
        {
            var productViewModels = await _context.Product
                .Include(c => c.CategoryDb)
                .Select(p => new ProductViewModelAlt
                {
                    Name = p.Name,
                    Price = p.Price,
                    Count = p.Count,
                    InventoryValue = p.Count * p.Price,
                    //Category = p.Category,
                    CategoryId = p.CategoryId,
                    CategoryDb = p.CategoryDb
                })
                //.Distinct()
                .ToListAsync();

            return View(productViewModels);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //
            //.Select(s => s.OrderByDescending(x => x.Date).FirstOrDefault());
            return _context.Product != null ?
                        View(await _context.Product.Include(c => c.CategoryDb).OrderByDescending(x => x.OrderDate).ToListAsync()) :
                        Problem("Entity set 'StorageContext.Product'  is null.");
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.Include(c => c.CategoryDb)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Price,OrderDate,Category,Shelf,Count,Description,CategoryId,CategoryDb")] Product product)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,OrderDate,Category,Shelf,Count,Description,CategoryId,CategoryDb")] Product product)
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

            var product = await _context.Product.Include(c => c.CategoryDb)
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
