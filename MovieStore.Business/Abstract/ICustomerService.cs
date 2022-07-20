using MovieStore.Core.Utilities.Results;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Abstract
{
    public interface ICustomerService
    {
        Task<IResult> Add(Customer customer);
        Task<IResult> Delete(int customerId);
        IDataResult<Customer> GetByMail(string mail);
        IDataResult<Customer> VerifyCustomer(string email, string password);
        IResult CheckIfCustomerEmailAlreadyExist(string email);
        Task<IResult> BuyMovie(Customer customer,MovieDetailDto movie);
        IDataResult<List<OrdersDto>> GetMyOrders(Customer customer);
    }
}
