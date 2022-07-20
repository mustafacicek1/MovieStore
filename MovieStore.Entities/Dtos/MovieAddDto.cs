using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Dtos
{
    public class MovieAddDto:IDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}
