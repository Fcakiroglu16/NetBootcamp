namespace bootcamp.Service.Products.DTOs
{
    //public record ProductDto(int Id, string Name, decimal Price, string Created);


    public class ProductDto
    {
        public ProductDto()
        {
        }

        public ProductDto(int id, string name, decimal price, string created)
        {
            Id = id;
            Name = name;
            Price = price;
            Created = created;
        }

        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }

        public string Created { get; set; } = default!;
    }
}