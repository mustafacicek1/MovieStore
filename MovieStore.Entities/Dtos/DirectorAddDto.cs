using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Dtos
{
    public class DirectorAddDto:IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
