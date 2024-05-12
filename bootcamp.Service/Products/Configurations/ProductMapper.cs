using AutoMapper;
using Bootcamp.Repository.Products;
using bootcamp.Service.Products.DTOs;

namespace bootcamp.Service.Products.Configurations
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            //.ForMember(x => x.Created, opt => opt.MapFrom(y => y.Created.ToShortDateString()))
            //.ForMember(x => x.Price, opt => opt.MapFrom(y => 200));
        }
    }
}