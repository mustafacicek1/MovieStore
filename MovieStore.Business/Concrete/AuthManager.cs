﻿using AutoMapper;
using MovieStore.Business.Abstract;
using MovieStore.Core.Utilities.Business;
using MovieStore.Core.Utilities.Results;
using MovieStore.Core.Utilities.Security.JWT;
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
    public class AuthManager : IAuthService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        public AuthManager(ICustomerService customerService,IMapper mapper,ITokenHelper tokenHelper)
        {
            _customerService = customerService;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> CustomerLogin(CustomerLoginDto customerLoginDto)
        {
            var result = _customerService.VerifyCustomer(customerLoginDto.Email, customerLoginDto.Password);
            if (!result.Success)
            {
                return new ErrorDataResult<AccessToken>(result.Message);
            }

            var customer = result.Data;
            string fullName = customer.Name + " " + customer.Surname;
            var token = _tokenHelper.CreateToken(fullName,customer.Email,"Customer");

            return new SuccessDataResult<AccessToken>(token);
        }

        public IResult CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            var result = _customerService.CheckIfCustomerEmailAlreadyExist(customerRegisterDto.Email);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            var customer = _mapper.Map<Customer>(customerRegisterDto);
            _customerService.Add(customer);
            return new SuccessResult("Kayıt Başarılı");
        }

        
    }
}