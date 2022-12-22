using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Count { get; set; }
        public int InventoryValue { get; set; }
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        [NotMapped]
        public Category Category { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? Categories { get; set; } = new List<SelectListItem>();
    }
}
