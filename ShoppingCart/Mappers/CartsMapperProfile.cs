using AutoMapper;
using Data.Models;
using SchoolOf.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers
{
    public class CartsMapperProfile : Profile
    {
        public CartsMapperProfile()
        {
            CreateMap<Cart, CartDto>().ForMember(destination => destination.Status, x = > x.MapFrom(SourceMemberNamingConvention => SourceMemberNamingConvention.Status.ToString()));
        }
    }
}
