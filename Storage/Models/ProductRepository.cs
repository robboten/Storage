using Microsoft.EntityFrameworkCore;
using Storage.Data;

namespace Storage.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly StorageContext storageContext;

        public ProductRepository(StorageContext storageContext)
        {
            this.storageContext = storageContext;
        }
        public IEnumerable<Product> AllProducts
        {
            get { return storageContext.Product.Include(p => p.CategoryDb); }
        }
        public Product? GetProductById(int id) => storageContext.Product.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> SearchProducts(string searchStr)
        {
            return storageContext.Product.Where(p => p.Name.Contains(searchStr));
        }
    }
}
