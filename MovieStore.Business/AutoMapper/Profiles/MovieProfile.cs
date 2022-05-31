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
    public class MovieProfile:Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieAddDto, Movie>();
            CreateMap<Movie, MoviesDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Movie, MovieDetailDto>()
               .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
               .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname));
               
        }
    }
}
