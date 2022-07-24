using MovieStore.Entities.Concrete;
using System.Collections.Generic;

namespace MovieStore.DataAccess.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        List<Order> GetAllOrders();
        List<Order> GetCustomerOrders(int customerId);
    }
}
