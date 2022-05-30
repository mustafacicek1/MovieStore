using MovieStore.Core.Utilities.Results;
using MovieStore.Core.Utilities.Security.JWT;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Abstract
{
    public interface IAuthService
    {
        IResult CustomerRegister(CustomerRegisterDto customerRegisterDto);
        IDataResult<AccessToken> CustomerLogin(CustomerLoginDto customerLoginDto);
    }
}
