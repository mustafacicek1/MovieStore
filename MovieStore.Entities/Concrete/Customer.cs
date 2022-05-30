using MovieStore.Core.Entities.Abstract;
using System.Collections.Generic;

namespace MovieStore.Entities.Concrete
{
    public class Customer:IEntity
    {
        public Customer()
        {
            CustomerMovies = new HashSet<CustomerMovie>();
            CustomerGenres = new HashSet<CustomerGenre>();
            Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<CustomerMovie> CustomerMovies { get; set; }
        public ICollection<CustomerGenre> CustomerGenres { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
