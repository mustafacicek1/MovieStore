using AutoMapper;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.AutoMapper.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrdersDto>()
               .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.Name+" "+src.Customer.Surname))
               .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Name));
        }
    }
}
