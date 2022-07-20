using MovieStore.Core.Entities.Abstract;
using System.Collections.Generic;

namespace MovieStore.Entities.Dtos
{
    public class MovieDetailDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public List<string> Actors { get; set; }
    }
}
