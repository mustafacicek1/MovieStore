using FluentValidation;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.ValidationRules.FluentValidation
{
    public class CustomerRegisterDtoValidator:AbstractValidator<CustomerRegisterDto>
    {
        public CustomerRegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(12);
            RuleFor(x=> x.RePassword).Equal(x => x.Password).WithMessage("Passwords must match");
        }
    }
}
