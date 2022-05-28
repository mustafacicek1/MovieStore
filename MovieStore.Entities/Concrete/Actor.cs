using MovieStore.Core.Entities.Abstract;
using System.Collections.Generic;

namespace MovieStore.Entities.Concrete
{
    public class Actor:IEntity
    {
        public Actor()
        {
            MovieActors=new HashSet<MovieActor>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
