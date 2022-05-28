using MovieStore.Core.DataAccess;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Abstract
{
    public interface IMovieRepository:IEntityRepository<Movie>
    {
    }
}
