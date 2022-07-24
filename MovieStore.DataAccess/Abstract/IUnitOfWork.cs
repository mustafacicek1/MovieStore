using System.Threading.Tasks;

namespace MovieStore.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        IActorRepository Actors {get;}
        ICustomerGenreRepository CustomerGenres { get; }
        ICustomerRepository Customers { get; }
        IDirectorRepository Directors { get; }
        IMovieActorRepository MovieActors { get; }
        IMovieRepository Movies { get; }
        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync();
    }
}
