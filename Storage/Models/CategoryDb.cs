using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class CategoryDb
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; }
    }
}
