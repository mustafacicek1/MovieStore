using MovieStore.Core.Entities.Abstract;
using System.Collections.Generic;

namespace MovieStore.Entities.Dtos
{
    public class DirectorDetailDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}
