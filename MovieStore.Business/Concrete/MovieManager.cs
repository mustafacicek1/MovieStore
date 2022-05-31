﻿using AutoMapper;
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
        public MovieManager(IUnitOfWork unitOfWork, IMapper mapper)
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

        public IResult SetStatus(int movieId)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExistForSetStatus(movieId));
            if (result != null)
            {
                return result;
            }

            var movie = _unitOfWork.Movies.Get(x => x.Id == movieId);
            movie.Status=movie.Status==false?movie.Status=true:movie.Status=false;
            _unitOfWork.SaveChanges();
            return new SuccessResult("Movie status changed!");
        }

        public IDataResult<List<MoviesDto>> GetAll()
        {
            var movies = _unitOfWork.Movies.GetAll(x=>x.Status==true,x=>x.Genre);
            return new SuccessDataResult<List<MoviesDto>>(_mapper.Map<List<MoviesDto>>(movies));
        }

        public IDataResult<MovieDetailDto> GetById(int movieId)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExist(movieId));
            if (result != null)
            {
                return new ErrorDataResult<MovieDetailDto>(result.Message);
            }

            var movie = _unitOfWork.Movies.Get(x => x.Id == movieId && x.Status==true, x => x.Director, x => x.Genre);
            var movieActors = _unitOfWork.MovieActors.GetAll(x => x.MovieId == movieId, x => x.Actor);
            MovieDetailDto vm = _mapper.Map<MovieDetailDto>(movie);
            foreach (var actor in movieActors)
            {
                vm.Actors.Add(actor.Actor.Name + " " + actor.Actor.Surname);
            }
            return new SuccessDataResult<MovieDetailDto>(vm);
        }

        public IResult Update(int movieId, MovieUpdateDto movieUpdateDto)
        {
            IResult result = BusinessRules.Run(CheckIfMovieIdDontExist(movieId),
                CheckIfMovieAlreadyExistForUpdate(movieId, movieUpdateDto.Name));
            if (result != null)
            {
                return result;
            }

            var movie = _unitOfWork.Movies.Get(x => x.Id == movieId);
            movie.Name = movieUpdateDto.Name == default ? movie.Name : movieUpdateDto.Name;
            movie.GenreId = movieUpdateDto.GenreId == default ? movie.GenreId : movieUpdateDto.GenreId;
            movie.DirectorId = movieUpdateDto.DirectorId == default ? movie.DirectorId : movieUpdateDto.DirectorId;
            movie.Price = movieUpdateDto.Price == default ? movie.Price : movieUpdateDto.Price;
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

        private IResult CheckIfMovieAlreadyExistForUpdate(int movieId, string movieName)
        {
            var movie = _unitOfWork.Movies.Get(x => x.Name == movieName && x.Id != movieId);
            if (movie is not null)
            {
                return new ErrorResult("Movie already exist");
            }

            return new SuccessResult();
        }

        private IResult CheckIfMovieIdDontExist(int movieId)
        {
            var movie = _unitOfWork.Movies.Get(x => x.Id == movieId &&x.Status==true);
            if (movie is null)
                return new ErrorResult("Movie not found");

            return new SuccessResult();
        }

        private IResult CheckIfMovieIdDontExistForSetStatus(int movieId)
        {
            var movie = _unitOfWork.Movies.Get(x => x.Id == movieId);
            if (movie is null)
                return new ErrorResult("Movie not found");

            return new SuccessResult();
        }

        public IDataResult<List<MoviesDto>> GetInActiveMovies()
        {
            var inActiveMovies =_unitOfWork.Movies.GetAll(x => x.Status == false, x => x.Genre);
            return new SuccessDataResult<List<MoviesDto>>(_mapper.Map<List<MoviesDto>>(inActiveMovies));
        }
    }
}
