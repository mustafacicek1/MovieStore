﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Business.Abstract;
using MovieStore.Core.Extensions;

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
        public IActionResult Delete(int id)
        {
            var result = _customerService.Delete(id);
            if (result.Success)
            {
                return NoContent();
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

            var result = _customerService.GetMyOrders(customer.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Authorize(Roles ="Customer")]
        [HttpPost("buymovie/{id}")]
        public IActionResult BuyMovie(int id)
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

            var result = _customerService.BuyMovie(customer.Data, movie.Data);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}