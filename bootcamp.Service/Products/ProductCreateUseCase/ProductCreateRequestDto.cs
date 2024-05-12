using System.ComponentModel.DataAnnotations;

namespace NetBootcamp.API.Products.ProductCreateUseCase
{
    #region Built-in Model Validation

    //public record ProductCreateRequestDto(
    //    [Required(ErrorMessage = "ürün ismi gereklidir")]
    //    [StringLength(10, ErrorMessage = "isim alanı en fazla 10 karakter olabilir.")]
    //    string Name,
    //    [Range(1, int.MaxValue)] decimal Price,
    //    [Url(ErrorMessage = "url formatı yanlış")]
    //    string Url); 

    #endregion


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