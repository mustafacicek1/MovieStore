using FluentValidation;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.ValidationRules.FluentValidation
{
    public class MovieUpdateDtoValidator:AbstractValidator<MovieUpdateDto>
    {
        public MovieUpdateDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(25).When(x=>!string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.Price).GreaterThan(0).When(x=>x.Price!=default);
            RuleFor(x => x.GenreId).GreaterThan(0).When(x => x.GenreId != default);
            RuleFor(x => x.DirectorId).GreaterThan(0).When(x => x.DirectorId != default);
        }
    }
}
