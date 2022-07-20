using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Dtos
{
    public class CustomerLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
