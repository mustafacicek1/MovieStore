using MovieStore.Core.Utilities.Results;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Abstract
{
    public interface IMovieService
    {
        Task<IResult> Add(MovieAddDto movieAddDto);
        Task<IResult> SetStatus(int movieId);
        Task<IResult> Update(int movieId,MovieUpdateDto movieUpdateDto);
        IDataResult<MovieDetailDto> GetById(int movieId);
        IDataResult<List<MoviesDto>> GetAll();
        IDataResult<List<MoviesDto>> GetInActiveMovies();
    }
}
