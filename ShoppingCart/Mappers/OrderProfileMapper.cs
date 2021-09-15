using AutoMapper;
using Data.Models;
using SchoolOf.Dtos;

namespace Mappers
{
    public class OrderProfileMapper : Profile
    {
        public OrderProfileMapper()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
