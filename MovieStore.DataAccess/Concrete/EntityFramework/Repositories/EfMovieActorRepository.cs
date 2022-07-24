using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfMovieActorRepository : GenericRepository<MovieActor>, IMovieActorRepository
    {
        public EfMovieActorRepository(MovieStoreDbContext context):base(context)
        {

        }
    }
}
