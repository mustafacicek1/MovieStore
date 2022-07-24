using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfMovieRepository:GenericRepository<Movie>,IMovieRepository
    {
        public EfMovieRepository(MovieStoreDbContext context):base(context)
        {
        }

        public List<Movie> GetAllMovies()
        {
            return _context.Movies.Where(x => x.Status == true).Include(x => x.Genre).ToList();
        }

        public List<Movie> GetInactiveMovies()
        {
            return _context.Movies.Where(x => x.Status == false).Include(x => x.Genre).ToList();
        }

        public Movie GetMovieDetails(int movieId)
        {
            return _context.Movies.Where(x => x.Id == movieId && x.Status == true)
                .Include(x => x.Director).Include(x => x.Genre).Include(x => x.MovieActors).ThenInclude(x => x.Actor).
                FirstOrDefault();
        }
    }
}
