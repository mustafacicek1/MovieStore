using AutoMapper;
using MovieStore.Business.Abstract;
using MovieStore.Core.Utilities.Business;
using MovieStore.Core.Utilities.Results;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;
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
        private readonly IMapper _mapper;
        public CustomerManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public IResult BuyMovie(Customer customer,MovieDetailDto movie)
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

            _unitOfWork.Orders.Add(order);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Movie bought successfully");
        }

        private IResult CheckIfOrderAlreadyExist(int customerId,int movieId)
        {
            var order = _unitOfWork.Orders.Get(x => x.CustomerId == customerId && x.MovieId == movieId);
            if (order!=null)
            {
                return new ErrorResult("You already have this movie");
            }
            return new SuccessResult();
        }
        public IDataResult<Customer> GetByMail(string mail)
        {
            var customer = _unitOfWork.Customers.Get(x => x.Email == mail);
            if (customer is null)
            {
                return new ErrorDataResult<Customer>("Customer not found");
            }
            return new SuccessDataResult<Customer>(customer);
        }

        public IDataResult<List<OrdersDto>> GetMyOrders(Customer customer)
        {
            var customerOrders = _unitOfWork.Orders.GetAll(x => x.CustomerId == customer.Id,x=>x.Customer,x=>x.Movie);
            return new SuccessDataResult<List<OrdersDto>>(_mapper.Map<List<OrdersDto>>(customerOrders));
        }
    }
}
