using ProductManagementSystem.Models;

namespace ProductManagementSystem.Services
{
    public class ProductRepository : IproductRepository
    {
        private List<Product> products;

        public ProductRepository()
        {
            products = new List<Product>();
            products.Add(new Product() { Id = 1, Name = "Apple", Description = "Fiji", Image = "apple.jpeg", Category = Cat.Fruit, Price = 0.10 });
            products.Add(new Product() { Id = 2, Name = "Orange", Description = "Florida", Image = "orange.jpeg", Category = Cat.Fruit, Price = 0.15 });
            products.Add(new Product() { Id = 3, Name = "Blueberries", Description = "Organic", Image = "blueberries.jpeg", Category = Cat.Fruit, Price = 3.00 });
            products.Add(new Product() { Id = 4, Name = "Kiwi", Description = "Organic", Image = "kiwi.jpeg", Category = Cat.Fruit, Price = 0.30 });
        }
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void DeleteProduct(int? id)
        {
            var protodelete = products.Find(x => x.Id == id);
            if (protodelete != null)
            {
                products.Remove(protodelete);
            }
        }

        public int GetMaxId()
        {
            int maxid = products.Max(x => x.Id);
            return maxid + 1;
        }

        public Product GetProduct(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return products.Find(x => x.Id == id);
            }
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public void UpdateProduct(Product product)
        {
            var protoupdate = products.Find(x => x.Id == product.Id);
            if (protoupdate != null)
            {
                protoupdate.Id = product.Id;
                protoupdate.Name = product.Name;
                protoupdate.Image = product.Image;
                protoupdate.Price = product.Price;
                protoupdate.Description = product.Description;
                protoupdate.Category = product.Category; 
            }
        }
    }
}
