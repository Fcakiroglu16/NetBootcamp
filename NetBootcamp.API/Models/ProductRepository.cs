using NetBootcamp.API.Controllers;

namespace NetBootcamp.API.Models
{
    public class ProductRepository
    {
        private static List<Product> _products =
        [
            new Product { Id = 1, Name = "Product 1", Price = 100 },
            new Product { Id = 2, Name = "Product 2", Price = 200 },
            new Product { Id = 3, Name = "Product 3", Price = 300 }
        ];

        public IReadOnlyList<Product> GetAll()
        {
            return _products;
        }

        public void Update(Product product)
        {
            var index = _products.FindIndex(x => x.Id == product.Id);

            _products[index] = product;
        }

        public void Create(Product product)
        {
            var methodName = nameof(ProductsController.GetById); // GetById
            _products.Add(product);
        }


        public Product? GetById(int id)
        {
            return _products.Find(x => x.Id == id);
        }

        // write delete method
        public void Delete(int id)
        {
            var product = GetById(id);

            _products.Remove(product!);
        }
    }
}