using FluentValidation;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.ValidationRules.FluentValidation
{
    public class DirectorUpdateDtoValidator:AbstractValidator<DirectorUpdateDto>
    {
        public DirectorUpdateDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(25).When(x => !string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.Surname).MaximumLength(25).When(x => !string.IsNullOrEmpty(x.Surname));
        }
    }
}
