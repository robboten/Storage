using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Price should be between 0 and 10000"),DataType(DataType.Currency)]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        //[Required]
        public string? Category { get; set; }
        public string? Shelf { get; set; }
        [Required]
        public int Count { get; set; }
        public string? Description { get; set; }
        [Required]
        public Category CategoryEnum { get; set; }
    }
}
