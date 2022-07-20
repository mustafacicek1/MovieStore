using MovieStore.Core.DataAccess;
using MovieStore.Entities.Concrete;
using System.Collections.Generic;

namespace MovieStore.DataAccess.Abstract
{
    public interface IMovieRepository:IGenericRepository<Movie>
    {
    }
}
