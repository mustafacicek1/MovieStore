using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Dtos
{
    public class MoviesDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
    }
}
