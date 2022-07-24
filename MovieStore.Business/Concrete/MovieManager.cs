using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Business.Abstract;
using MovieStore.Business.ValidationRules.FluentValidation;
using MovieStore.Core.CrossCuttingConcerns.Validation;
using MovieStore.Core.Utilities.Business;
using MovieStore.Core.Utilities.Results;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MovieManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(MovieAddDto movieAddDto)
        {
            ValidationTool.Validate(new MovieAddDtoValidator(), movieAddDto);

            IResult result = BusinessRules.Run(CheckIfMovieAlreadyExist(movieAddDto.Name));
            if (result != null)
            {
                return result;
            }

            var movie = _mapper.Map<Movie>(movieAddDto);
            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Movie added");
        }

        public async Task<IResult> SetStatus(int movieId)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExistForSetStatus(movieId));
            if (result != null)
            {
                return result;
            }

            var movie = await _unitOfWork.Movies.GetByIdAsync(movieId);
            movie.Status=movie.Status==false?movie.Status=true:movie.Status=false;
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Movie status changed!");
        }

        public IDataResult<List<MoviesDto>> GetAll()
        {
            var movies = _unitOfWork.Movies.GetAllMovies();
            return new SuccessDataResult<List<MoviesDto>>(_mapper.Map<List<MoviesDto>>(movies));
        }

        public IDataResult<MovieDetailDto> GetById(int movieId)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExist(movieId));
            if (result != null)
            {
                return new ErrorDataResult<MovieDetailDto>(result.Message);
            }

            var movie = _unitOfWork.Movies.GetMovieDetails(movieId);
            MovieDetailDto vm = _mapper.Map<MovieDetailDto>(movie);
            return new SuccessDataResult<MovieDetailDto>(vm);
        }

        public async Task<IResult> Update(int movieId, MovieUpdateDto movieUpdateDto)
        {
            ValidationTool.Validate(new MovieUpdateDtoValidator(),movieUpdateDto);

            IResult result = BusinessRules.Run(CheckIfMovieIdDontExist(movieId),
                CheckIfMovieAlreadyExistForUpdate(movieId, movieUpdateDto.Name));
            if (result != null)
            {
                return result;
            }

            var movie = await _unitOfWork.Movies.GetByIdAsync(movieId);
            movie.Name = movieUpdateDto.Name == default ? movie.Name : movieUpdateDto.Name;
            movie.GenreId = movieUpdateDto.GenreId == default ? movie.GenreId : movieUpdateDto.GenreId;
            movie.DirectorId = movieUpdateDto.DirectorId == default ? movie.DirectorId : movieUpdateDto.DirectorId;
            movie.Price = movieUpdateDto.Price == default ? movie.Price : movieUpdateDto.Price;
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Movie updated");
        }

        private IResult CheckIfMovieAlreadyExist(string movieName)
        {
            var movie = _unitOfWork.Movies.Where(x => x.Name == movieName).FirstOrDefault();
            if (movie is not null)
            {
                return new ErrorResult("Movie already exist");
            }

            return new SuccessResult();
        }

        private IResult CheckIfMovieAlreadyExistForUpdate(int movieId, string movieName)
        {
            var movie = _unitOfWork.Movies.Where(x => x.Name == movieName && x.Id != movieId).FirstOrDefault();
            if (movie is not null)
            {
                return new ErrorResult("Movie already exist");
            }

            return new SuccessResult();
        }

        private IResult CheckIfMovieIdDontExist(int movieId)
        {
            var movie = _unitOfWork.Movies.Where(x => x.Id == movieId &&x.Status==true).FirstOrDefault();
            if (movie is null)
                return new ErrorResult("Movie not found");

            return new SuccessResult();
        }

        private IResult CheckIfMovieIdDontExistForSetStatus(int movieId)
        {
            var movie = _unitOfWork.Movies.Where(x => x.Id == movieId).FirstOrDefault();
            if (movie is null)
                return new ErrorResult("Movie not found");

            return new SuccessResult();
        }

        public IDataResult<List<MoviesDto>> GetInActiveMovies()
        {
            var inActiveMovies = _unitOfWork.Movies.GetInactiveMovies();
            return new SuccessDataResult<List<MoviesDto>>(_mapper.Map<List<MoviesDto>>(inActiveMovies));
        }
    }
}
