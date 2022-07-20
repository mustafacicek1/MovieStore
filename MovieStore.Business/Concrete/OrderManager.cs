using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Business.Abstract;
using MovieStore.Core.Utilities.Results;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var orders = _unitOfWork.Orders.GetAll().Include(x=>x.Movie).Include(x=>x.Customer);
            return new SuccessDataResult<List<OrdersDto>>(_mapper.Map<List<OrdersDto>>(orders));
        }
    }
}
