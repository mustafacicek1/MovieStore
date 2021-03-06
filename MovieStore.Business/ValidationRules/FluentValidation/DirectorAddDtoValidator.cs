using FluentValidation;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.ValidationRules.FluentValidation
{
    public class DirectorAddDtoValidator:AbstractValidator<DirectorAddDto>
    {
        public DirectorAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(25);
        }
    }
}
