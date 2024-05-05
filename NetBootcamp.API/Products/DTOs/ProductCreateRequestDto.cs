namespace NetBootcamp.API.Products.DTOs
{
    // yeni hal
    public record ProductCreateRequestDto(string Name, decimal Price);

    //eski hal
    //public class ProductCreateRequestDtoLegacy
    //{
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }


    //public A(){}

    //    public A(string name,decimal price)
    //    {
    //        Name = name;
    //        Price = price;
    //    }

    //}
}