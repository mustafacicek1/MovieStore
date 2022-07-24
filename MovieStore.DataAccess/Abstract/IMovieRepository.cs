using MovieStore.Entities.Concrete;
using System.Collections.Generic;

namespace MovieStore.DataAccess.Abstract
{
    public interface IMovieRepository:IGenericRepository<Movie>
    {
        Movie GetMovieDetails(int movieId);
        List<Movie> GetAllMovies();
        List<Movie> GetInactiveMovies();
    }
}
