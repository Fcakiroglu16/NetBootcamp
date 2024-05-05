namespace NetBootcamp.API.Products.DTOs
{
    public record ProductDto(int Id, string Name, decimal Price, string Created);

    //public record ProductDto
    //{
    //    public int Id { get; init; }
    //    public string Name { get; init; } = default!;
    //    public decimal Price { get; init; }

    //    public string Created { get; init; } = default!;
    //}
}