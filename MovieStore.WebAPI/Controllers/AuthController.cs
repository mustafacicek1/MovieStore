using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Business.Abstract;
using MovieStore.Entities.Dtos;

namespace MovieStore.WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("customer/register")]
        public IActionResult CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            var result = _authService.CustomerRegister(customerRegisterDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("customer/login")]
        public IActionResult CustomerLogin(CustomerLoginDto customerLoginDto)
        {
            var result = _authService.CustomerLogin(customerLoginDto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
