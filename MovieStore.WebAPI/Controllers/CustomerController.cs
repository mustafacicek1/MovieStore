using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Business.Abstract;
using MovieStore.Core.Extensions;
using System.Threading.Tasks;

namespace MovieStore.WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMovieService _movieService;

        public CustomerController(ICustomerService customerService, IMovieService movieService)
        {
            _customerService = customerService;
            _movieService = movieService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.Delete(id);
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Message);
        }

        [Authorize(Roles ="Customer")]
        [HttpPost("buymovie/{id}")]
        public async Task<IActionResult> BuyMovie(int id)
        {
            var customer = _customerService.GetByMail(HttpContext.User.GetAuthenticatedUserEmail());
            if (!customer.Success)
            {
                return BadRequest(customer.Message);
            }

            var movie = _movieService.GetById(id);
            if (!movie.Success)
            {
                return BadRequest(movie.Message);
            }

            var result = await _customerService.BuyMovie(customer.Data, movie.Data);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
