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
    public class ActorProfile:Profile
    {
        public ActorProfile()
        {
            CreateMap<ActorAddDto, Actor>();
            CreateMap<Actor, ActorsDto>();
            CreateMap<Actor, ActorDetailDto>();
        }
    }
}
