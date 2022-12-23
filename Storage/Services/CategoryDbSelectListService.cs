using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;

namespace Storage.Services
{
    public class CategoryDbSelectListService : ICategorySelectListService
    {
        private StorageContext context;

        public CategoryDbSelectListService(StorageContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            return await context.CategoryDb.Distinct()
            //return await context.Product.Include(c => c.CategoryDb).Select(p=>p.CategoryDb).Distinct()
                                .Select(g => new SelectListItem
                                {
                                    Text = g.Name.ToString(),
                                    Value = g.Id.ToString()
                                })
                                .ToListAsync();

        }
    }
}
