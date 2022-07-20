using AutoMapper;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;
using System.Linq;

namespace MovieStore.Business.AutoMapper.Profiles
{
    public class DirectorProfile:Profile
    {
        public DirectorProfile()
        {
            CreateMap<DirectorAddDto, Director>();
            CreateMap<Director, DirectorsDto>();
            CreateMap<Director, DirectorDetailDto>()
                .ForMember(dest=>dest.Movies,opt=>opt.MapFrom(src=>src.Movies.Select(x=>x.Name)));
        }
    }
}
