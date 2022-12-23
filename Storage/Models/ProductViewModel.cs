using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        //[NotMapped]
        //public Category? Category { get; set; }
        public int? CategoryId { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? Categories { get; set; } = new List<SelectListItem>();
        [NotMapped]
        public IEnumerable<SelectListItem>? CategoriesDb { get; set; } = new List<SelectListItem>();
    }

    public class ProductViewModelAlt
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Count { get; set; }
        [Display(Name = "Inventory Value")]
        public int InventoryValue { get; set; }
        //[NotMapped]
        //public Category Category { get; set; }
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryDb? CategoryDb { get; set; }
    }

}
