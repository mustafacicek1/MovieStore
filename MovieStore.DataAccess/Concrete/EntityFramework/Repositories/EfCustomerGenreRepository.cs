using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCustomerGenreRepository : GenericRepository<CustomerGenre>, ICustomerGenreRepository
    {
        public EfCustomerGenreRepository(MovieStoreDbContext context):base(context)
        {

        }
    }
}
