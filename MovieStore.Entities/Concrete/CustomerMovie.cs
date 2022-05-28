using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Concrete
{
    public class CustomerMovie:IEntity
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }

        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
