using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Abstract
{
    public interface IDirectorRepository : IGenericRepository<Director>
    {
        Director GetDirectorDetails(int directorId);
    }
}
