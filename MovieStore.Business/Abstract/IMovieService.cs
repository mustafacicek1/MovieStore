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
        IResult Add(MovieAddDto movieAddDto);
        IResult SetStatus(int movieId);
        IResult Update(int movieId,MovieUpdateDto movieUpdateDto);
        IDataResult<MovieDetailDto> GetById(int movieId);
        IDataResult<List<MovieDetailDto>> GetAll();
        IDataResult<List<MovieDetailDto>> GetInActiveMovies();
    }
}
