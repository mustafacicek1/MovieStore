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
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExist(movieId));
            if (result != null)
            {
                return result;
            }

            var movie = _unitOfWork.Movies.Get(x=>x.Id== movieId);
            _unitOfWork.Movies.Delete(movie);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Movie deleted!");
        }

        public IDataResult<List<MovieDetailDto>> GetAll()
        {
            var movies = _unitOfWork.Movies.GetAll(null,x=>x.Director,x=>x.Genre);
            return new SuccessDataResult<List<MovieDetailDto>>(_mapper.Map<List<MovieDetailDto>>(movies));
        }

        public IDataResult<MovieDetailDto> GetById(int movieId)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExist(movieId));
            if (result != null)
            {
                return new ErrorDataResult<MovieDetailDto>(result.Message);
            }

            var movie = _unitOfWork.Movies.Get(x=>x.Id== movieId,x=>x.Director,x=>x.Genre);
            return new SuccessDataResult<MovieDetailDto>(_mapper.Map<MovieDetailDto>(movie));
        }

        public IResult Update(int movieId,MovieUpdateDto movieUpdateDto)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExist(movieId));
            if(result!=null)
            {
                return result;
            }

            var movie = _mapper.Map<Movie>(movieUpdateDto);
            movie.Id = movieId;
            _unitOfWork.Movies.Update(movie);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Movie updated");
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

        private IResult CheckIfMovieIdDontExist(int movieId)
        {
            var movie = _unitOfWork.Movies.Get(x => x.Id == movieId);
            if (movie is null)
                return new ErrorResult("Movie not found");

            return new SuccessResult();
        }
    }
}
