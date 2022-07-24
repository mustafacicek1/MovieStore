using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Abstract
{
    public interface IActorRepository : IGenericRepository<Actor>
    {
        Actor GetActorDetails(int actorId);
    }
}
