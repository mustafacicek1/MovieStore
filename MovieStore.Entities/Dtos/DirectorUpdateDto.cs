using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Dtos
{
    public class DirectorUpdateDto:IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
