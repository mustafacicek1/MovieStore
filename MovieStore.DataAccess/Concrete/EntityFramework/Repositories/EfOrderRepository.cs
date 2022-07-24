using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfOrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public EfOrderRepository(MovieStoreDbContext context):base(context)
        {

        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Include(x => x.Movie).Include(x => x.Customer).ToList();
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
           return _context.Orders.Where(x => x.CustomerId == customerId).Include(x => x.Customer).Include(x => x.Movie).ToList();
        }
    }
}
