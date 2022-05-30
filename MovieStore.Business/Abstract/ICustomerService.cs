using MovieStore.Core.Utilities.Results;
using MovieStore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Delete(int customerId);
        IDataResult<Customer> VerifyCustomer(string email, string password);
        IResult CheckIfCustomerEmailAlreadyExist(string email);
    }
}
