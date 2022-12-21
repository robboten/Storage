using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;

namespace Storage.Services
{
    public class CategorySelectListService : ICategorySelectListService
    {
        private StorageContext context;

        public CategorySelectListService(StorageContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            return await context.Product.Select(p=>p.Category).Distinct()
                                .Select(g => new SelectListItem
                                {
                                    Text = g.ToString(),
                                    Value = g.ToString()
                                })
                                .ToListAsync();

        }
    }
}
