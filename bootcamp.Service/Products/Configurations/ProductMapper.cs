using AutoMapper;
using Bootcamp.Repository.Products;
using bootcamp.Service.Products.DTOs;
using bootcamp.Service.Products.Helpers;

namespace bootcamp.Service.Products.Configurations
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>()
                .ForPath(x => x.Created, opt => opt.MapFrom(y => y.Created.ToShortDateString()))
                .ForPath(x => x.Price, opt => opt.MapFrom(y => new PriceCalculator().CalculateKdv(y.Price, 1.20m)));
        }
    }
}