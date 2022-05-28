using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Concrete
{
    public class CustomerGenre:IEntity
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }

        public Customer Customer { get; set; }
        public Genre Genre { get; set; }
    }
}
