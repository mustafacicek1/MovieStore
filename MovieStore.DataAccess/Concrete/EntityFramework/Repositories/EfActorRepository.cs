using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using System.Linq;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public EfActorRepository(MovieStoreDbContext context):base(context)
        {

        }

        public Actor GetActorDetails(int actorId)
        {
            return _context.Actors.Where(x => x.Id == actorId).Include(x => x.MovieActors).ThenInclude(x => x.Movie).FirstOrDefault();
        }
    }
}
