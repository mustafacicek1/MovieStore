using MovieStore.Business.Abstract;
using MovieStore.Core.Utilities.Business;
using MovieStore.Core.Utilities.Results;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;
using System;
using System.Linq;
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

        public async Task<IResult> Add(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult();
        }

        public async Task<IResult> Delete(int customerId)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerIdNotExist(customerId));
            if (result!=null)
            {
                return result;
            }
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            _unitOfWork.Customers.Remove(customer);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult();
        }

        public IResult CheckIfCustomerEmailAlreadyExist(string email)
        {
            var customer = _unitOfWork.Customers.Where(x=>x.Email==email).FirstOrDefault();
            if (customer != null)
            {
                return new ErrorResult("This email already registered");
            }
            return new SuccessResult();
        }

        public IDataResult<Customer> VerifyCustomer(string email,string password)
        {
            var customer = _unitOfWork.Customers.Where(x => x.Email == email&& x.Password == password).FirstOrDefault();
            if (customer is null)
            {
                return new ErrorDataResult<Customer>("Email or password incorrect");
            }
            return new SuccessDataResult<Customer>(customer);
        }

        private IResult CheckIfCustomerIdNotExist(int customerId)
        {
            var customer = _unitOfWork.Customers.Where(x=>x.Id==customerId).FirstOrDefault();
            if (customer is null)
            {
                return new ErrorResult("Customer not found");
            }

            return new SuccessResult();
        }

        public async Task<IResult> BuyMovie(Customer customer,MovieDetailDto movie)
        {
            IResult result = BusinessRules.Run(CheckIfOrderAlreadyExist(customer.Id,movie.Id));
            if (result!=null)
            {
                return result;
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                MovieId = movie.Id,
                Price = movie.Price,
                OrderDate = DateTime.Now
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Movie bought successfully");
        }

        private IResult CheckIfOrderAlreadyExist(int customerId,int movieId)
        {
            var order = _unitOfWork.Orders.Where(x => x.CustomerId == customerId && x.MovieId == movieId).FirstOrDefault();
            if (order!=null)
            {
                return new ErrorResult("You already have this movie");
            }
            return new SuccessResult();
        }
        public IDataResult<Customer> GetByMail(string mail)
        {
            var customer = _unitOfWork.Customers.Where(x => x.Email == mail).FirstOrDefault();
            if (customer is null)
            {
                return new ErrorDataResult<Customer>("Customer not found");
            }
            return new SuccessDataResult<Customer>(customer);
        }
    }
}
