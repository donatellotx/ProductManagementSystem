using ProductManagementSystem.Models;

namespace ProductManagementSystem.Services
{
    public class DBProductRepository : IproductRepository
    {
        private ProductContext _productContext;
        public DBProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public void AddProduct(Product product)
        {
            if(product.Category.ToString() == "Fruit")
            { product.CatId = 1; }
            if (product.Category.ToString() == "Vegetable")
            { product.CatId = 2; }
            if (product.Category.ToString() == "Grain")
            { product.CatId = 3; }
            if (product.Category.ToString() == "Juice")
            { product.CatId = 4; }

            _productContext.Products.Add(product);
            _productContext.SaveChanges();
        }

        
        public void DeleteProduct(int? id)
        {
            var product = _productContext.Products.Find(id);
            if(product != null)
            {
                _productContext.Products.Remove(product);
                _productContext.SaveChanges();
            }
        }

        public int GetMaxId()
        {
            return _productContext.Products.Max(x => x.Id)+1;
        }

        public Product GetProduct(int? id)
        {
            return _productContext.Products.Find(id);
        }

        public List<Product> GetProducts()
        {
            return new List<Product>(_productContext.Products);
        }

        public void UpdateProduct(Product product)
        {
            var pro = _productContext.Products.Find(product.Id);
            if(pro != null)
            {
                pro.Id = product.Id;
                pro.Name = product.Name;
                pro.Price = product.Price;
                pro.Description = product.Description;
                pro.Category = product.Category;
                pro.Image = product.Image;
                if (product.Category.ToString() == "Fruit")
                { pro.CatId = 1; }
                if (product.Category.ToString() == "Vegetable")
                { pro.CatId = 2; }
                if (product.Category.ToString() == "Grain")
                { pro.CatId = 3; }
                if (product.Category.ToString() == "Juice")
                { pro.CatId = 4; }

                _productContext.SaveChanges();

            }
        }
    }
}
