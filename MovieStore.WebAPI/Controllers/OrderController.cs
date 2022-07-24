using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Business.Abstract;
using MovieStore.Core.Extensions;

namespace MovieStore.WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("myorders")]
        public IActionResult GetMyOrders()
        {
            var customer = _customerService.GetByMail(HttpContext.User.GetAuthenticatedUserEmail());
            if (!customer.Success)
            {
                return BadRequest(customer.Message);
            }

            var result = _orderService.GetCustomerOrders(customer.Data.Id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
