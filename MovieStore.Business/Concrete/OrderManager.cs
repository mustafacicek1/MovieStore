using AutoMapper;
using MovieStore.Business.Abstract;
using MovieStore.Core.Utilities.Results;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Dtos;
using System.Collections.Generic;

namespace MovieStore.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IDataResult<List<OrdersDto>> GetAll()
        {
            var orders = _unitOfWork.Orders.GetAllOrders();
            return new SuccessDataResult<List<OrdersDto>>(_mapper.Map<List<OrdersDto>>(orders));
        }

        public IDataResult<List<OrdersDto>> GetCustomerOrders(int customerId)
        {
            var customerOrders = _unitOfWork.Orders.GetCustomerOrders(customerId);
            return new SuccessDataResult<List<OrdersDto>>(_mapper.Map<List<OrdersDto>>(customerOrders));
        }
    }
}
