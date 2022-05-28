

namespace MovieStore.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        IActorRepository Actors {get;}
        ICustomerGenreRepository CustomerGenres { get; }
        ICustomerMovieRepository CustomerMovies { get; }
        ICustomerRepository Customers { get; }
        IDirectorRepository Directors { get; }
        IMovieActorRepository MovieActors { get; }
        IMovieRepository Movies { get; }
        IOrderRepository Orders { get; }
        int SaveChanges();
    }
}
