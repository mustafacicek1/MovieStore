using MovieStore.Business.Abstract;
using MovieStore.Core.Utilities.Business;
using MovieStore.Core.Utilities.Results;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IResult Add(Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.SaveChanges();
            return new SuccessResult();
        }

        public IResult Delete(int customerId)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerIdNotExist(customerId));
            if (result!=null)
            {
                return result;
            }
            var customer = _unitOfWork.Customers.Get(x => x.Id == customerId);
            _unitOfWork.Customers.Delete(customer);
            _unitOfWork.SaveChanges();
            return new SuccessResult();
        }

        public IResult CheckIfCustomerEmailAlreadyExist(string email)
        {
            var customer = _unitOfWork.Customers.Get(x => x.Email == email);
            if (customer != null)
            {
                return new ErrorResult("This email already registered");
            }
            return new SuccessResult();
        }

        public IDataResult<Customer> VerifyCustomer(string email,string password)
        {
            var customer = _unitOfWork.Customers.Get(x => x.Email == email&& x.Password == password);
            if (customer is null)
            {
                return new ErrorDataResult<Customer>("Email or password incorrect");
            }
            return new SuccessDataResult<Customer>(customer);
        }

        private IResult CheckIfCustomerIdNotExist(int customerId)
        {
            var customer = _unitOfWork.Customers.Get(x => x.Id == customerId);
            if (customer is null)
            {
                return new ErrorResult("Customer not found");
            }

            return new SuccessResult();
        }

        
    }
}
