using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        public string? CatName { get; set; }
        // 1 Category for a plethora of products
        public virtual ICollection<Product>? Products { get; set; }
    }
}
