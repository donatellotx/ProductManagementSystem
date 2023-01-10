using Microsoft.EntityFrameworkCore;

namespace ProductManagementSystem.Models
{
    // This class represeents the Data Base
    public class ProductContext : DbContext
    {

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set;} // Product Table
        public DbSet<Category> Categories { get; set;} // Category Table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Data Seeding
            modelBuilder.Entity<Category>().HasData(
                new Category { CatId = 1, CatName = "Fruit" },
                new Category { CatId = 2, CatName = "Vegetable" },
                new Category { CatId = 3, CatName = "Grain" },
                new Category { CatId = 4, CatName = "Juice" });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Apple", Description = "Fiji", Image = "apple.jpeg", Category = Cat.Fruit, Price = 0.10 },
                new Product { Id = 2, Name = "Orange", Description = "Florida", Image = "orange.jpeg", Category = Cat.Fruit, Price = 0.15 },
                new Product { Id = 3, Name = "Blueberries", Description = "Organic", Image = "blueberries.jpeg", Category = Cat.Fruit, Price = 3.00 },
                new Product { Id = 4, Name = "Kiwi", Description = "Organic", Image = "kiwi.jpeg", Category = Cat.Fruit, Price = 0.30 });



        }
    }
}
