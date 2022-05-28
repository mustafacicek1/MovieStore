using AutoMapper;
using MovieStore.Business.Abstract;
using MovieStore.Core.Utilities.Business;
using MovieStore.Core.Utilities.Results;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MovieManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IResult Add(MovieAddDto movieAddDto)
        {
            IResult result = BusinessRules.Run(CheckIfMovieAlreadyExist(movieAddDto.Name));
            if (result != null)
            {
                return result;
            }

            var movie = _mapper.Map<Movie>(movieAddDto);
            _unitOfWork.Movies.Add(movie);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Movie added");
        }

        public IResult Delete(int movieId)
        {
            var movie = _unitOfWork.Movies.Get(x=>x.Id== movieId);
            _unitOfWork.Movies.Delete(movie);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Movie deleted!");
        }

        public IDataResult<MovieDetailDto> GetById(int movieId)
        {
            var movie = _unitOfWork.Movies.Get(x=>x.Id== movieId,x=>x.Director,x=>x.Genre);
            return new SuccessDataResult<MovieDetailDto>(_mapper.Map<MovieDetailDto>(movie));
        }

        private IResult CheckIfMovieAlreadyExist(string movieName)
        {
            var movie = _unitOfWork.Movies.Get(x => x.Name == movieName);
            if (movie is not null)
            {
                return new ErrorResult("Movie already exist");
            }

            return new SuccessResult();
        }
    }
}
