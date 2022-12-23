using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Storage.Models
{
    public class CategoryDb
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}
