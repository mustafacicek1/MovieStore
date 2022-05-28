using MovieStore.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entities.Concrete
{
    public class Movie:IEntity
    {
        public Movie()
        {
            MovieActors = new HashSet<MovieActor>();
            Orders = new HashSet<Order>();
            CustomerMovies = new HashSet<CustomerMovie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }

        public Genre Genre { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<CustomerMovie> CustomerMovies { get; set; }
    }
}
