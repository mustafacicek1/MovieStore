using FluentValidation;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.ValidationRules.FluentValidation
{
    public class MovieAddDtoValidator:AbstractValidator<MovieAddDto>
    {
        public MovieAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.GenreId).GreaterThan(0);
            RuleFor(x => x.DirectorId).GreaterThan(0);
        }
    }
}
