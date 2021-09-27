
using AutoMapper;
using Data.Models;
using SchoolOf.Dtos;
using System.Linq;

namespace SchoolOf.Mappers
{
    public class OrdersMapperProfile : Profile
    {
        public OrdersMapperProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Total, x => x.MapFrom(y => y.Total > 0 ? y.Total : GetTotalPrice(y)));

            CreateMap<CreateOrderDto, Order>();
        }

        private decimal GetTotalPrice(Order y)
        {
            return y?.Cart?.Products?.Sum(x => x.Price) ?? 0;
        }
    }
}