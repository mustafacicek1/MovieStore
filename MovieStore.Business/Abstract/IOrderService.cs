using MovieStore.Core.Utilities.Results;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<OrdersDto>> GetAll();
        IDataResult<List<OrdersDto>> GetCustomerOrders(int customerId);
    }
}
