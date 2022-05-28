using MovieStore.Core.Entities.Abstract;
using System;

namespace MovieStore.Entities.Concrete
{
    public class Order:IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }
        public Movie Movie { get; set; }

    }
}
