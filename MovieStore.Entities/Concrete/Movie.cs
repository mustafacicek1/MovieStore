using MovieStore.Core.Entities.Abstract;
using System.Collections.Generic;

namespace MovieStore.Entities.Concrete
{
    public class Movie:IEntity
    {
        public Movie()
        {
            MovieActors = new HashSet<MovieActor>();
            Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public bool Status { get; set; } = true;

        public Genre Genre { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
