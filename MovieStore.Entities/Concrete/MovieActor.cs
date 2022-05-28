using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Concrete
{
    public class MovieActor:IEntity
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}
