using AutoMapper;
using Data.Models;
using SchoolOf.Dtos;

namespace Mappers
{
   public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
