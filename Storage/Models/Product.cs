using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Storage.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Price should be between 0 and 10000"), DataType(DataType.Currency)]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public DateTime? OrderDate { get; set; }
        public string? Shelf { get; set; }
        [Required]
        public int Count { get; set; }
        public string? Description { get; set; }
        //public Category Category { get; set; }

        [Display(Name = "Category Id")]

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryDb? CategoryDb { get; set; }
    }
}
