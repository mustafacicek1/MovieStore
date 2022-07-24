using AutoMapper;
using MovieStore.Business.Abstract;
using MovieStore.Business.ValidationRules.FluentValidation;
using MovieStore.Core.CrossCuttingConcerns.Validation;
using MovieStore.Core.Utilities.Results;
using MovieStore.Core.Utilities.Security.JWT;
using MovieStore.Entities.Concrete;
using MovieStore.Entities.Dtos;

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
            ValidationTool.Validate(new CustomerLoginDtoValidator(), customerLoginDto);

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
            ValidationTool.Validate(new CustomerRegisterDtoValidator(), customerRegisterDto);

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
