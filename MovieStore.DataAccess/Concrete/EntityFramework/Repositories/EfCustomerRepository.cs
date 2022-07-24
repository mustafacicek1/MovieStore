using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public EfCustomerRepository(MovieStoreDbContext context):base(context)
        {

        }
    }
}
