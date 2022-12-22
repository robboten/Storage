using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class CategoryDb
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
