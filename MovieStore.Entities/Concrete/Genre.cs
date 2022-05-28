using MovieStore.Core.Entities.Abstract;
using System.Collections.Generic;

namespace MovieStore.Entities.Concrete
{
    public class Genre:IEntity
    {
        public Genre()
        {
            Movies=new HashSet<Movie>();
            CustomerGenres = new HashSet<CustomerGenre>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
        public ICollection<CustomerGenre> CustomerGenres { get; set; }
    }
}
