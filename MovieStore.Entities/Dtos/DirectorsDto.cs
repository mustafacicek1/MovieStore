using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Dtos
{
    public class DirectorsDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
