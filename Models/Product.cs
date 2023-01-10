using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Models
{
    public enum Cat
    {
        Fruit,
        Vegetable,
        Grain,
        Juice

    }
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        
        public int Id { get; set; }
        public int CatId { get; set; } //Foreign Key

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please fill in the product name")]
        [MaxLength(75)]
        // Custom Validation
        [AllLetters(ErrorMessage = "Eneter letters only")]
        public string? Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public string? Image { get; set; }
        public Cat Category { get; set; }
    }
}
