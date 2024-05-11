using System.Runtime.CompilerServices;

namespace NetBootcamp.API.Products
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products =
        [
            new Product { Id = 1, Name = "Product 1", Price = 100 },
            new Product { Id = 2, Name = "Product 2", Price = 200 },
            new Product { Id = 3, Name = "Product 3", Price = 300 },
            new Product { Id = 4, Name = "Product 4", Price = 300 },
            new Product { Id = 5, Name = "Product 5", Price = 300 },
            new Product { Id = 6, Name = "Product 6", Price = 300 },
            new Product { Id = 7, Name = "Product 7", Price = 300 },
            new Product { Id = 9, Name = "Product 8", Price = 300 },
            new Product { Id = 9, Name = "Product 9", Price = 300 },
            new Product { Id = 10, Name = "Product 10", Price = 300 }
        ];


        public IReadOnlyList<Product> GetAll()
        {
            return _products;
        }

        public IReadOnlyList<Product> GetAllByPage(int page, int pageSize)
        {
            return _products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            // LinQ => Language Integrated Query
            // linQ to Collection=>
            // linQ to Entity


            //_products.First(x=>x.Id==20)


            // skip=> atla
            // take=> alma


            // 1,3 => 1,2,3 => ilk 3
            // 2,3 => 4,5,6 
            // 3,3 => 7,8,9


            //_products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(Product product)
        {
            var index = _products.FindIndex(x => x.Id == product.Id);

            _products[index] = product;
        }

        public void UpdateProductName(string name, int id)
        {
            var product = GetById(id);

            product!.Name = name;


            //var index = _products.FindIndex(x => x.Id == id);
            //_products[index].Name = name;
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