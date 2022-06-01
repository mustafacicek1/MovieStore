using AutoMapper;
using MovieStore.Business.Abstract;
using MovieStore.Business.ValidationRules.FluentValidation;
using MovieStore.Core.CrossCuttingConcerns.Validation;
using MovieStore.Core.Utilities.Business;
using MovieStore.Core.Utilities.Results;
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
    public class DirectorManager : IDirectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DirectorManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IResult Add(DirectorAddDto directorAddDto)
        {
            ValidationTool.Validate(new DirectorAddDtoValidator(), directorAddDto);

            var director = _mapper.Map<Director>(directorAddDto);
            _unitOfWork.Directors.Add(director);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Director added");
        }

        public IResult Delete(int directorId)
        {
            IResult result = BusinessRules.Run(CheckIfDirectorIdDontExist(directorId));
            if (result != null)
            {
                return result;
            }

            var director = _unitOfWork.Directors.Get(x => x.Id == directorId);
            _unitOfWork.Directors.Delete(director);
            _unitOfWork.SaveChanges();
            return new SuccessResult();
        }

        public IDataResult<List<DirectorsDto>> GetAll()
        {
            var directors = _unitOfWork.Directors.GetAll();
            return new SuccessDataResult<List<DirectorsDto>>(_mapper.Map<List<DirectorsDto>>(directors));
        }

        public IDataResult<DirectorDetailDto> GetById(int directorId)
        {
            IResult result = BusinessRules.Run(CheckIfDirectorIdDontExist(directorId));
            if (result != null)
            {
                return new ErrorDataResult<DirectorDetailDto>(result.Message);
            }

            var director = _unitOfWork.Directors.Get(x => x.Id == directorId,x=>x.Movies);
            DirectorDetailDto vm = _mapper.Map<DirectorDetailDto>(director);
            foreach (var movie in director.Movies)
            {
                vm.Movies.Add(movie.Name);
            }
            return new SuccessDataResult<DirectorDetailDto>(vm);
        }

        public IResult Update(int directorId, DirectorUpdateDto directorUpdateDto)
        {
            ValidationTool.Validate(new DirectorUpdateDtoValidator(), directorUpdateDto);

            IResult result = BusinessRules.Run(CheckIfDirectorIdDontExist(directorId));
            if (result != null)
            {
                return result;
            }

            var director = _unitOfWork.Directors.Get(x => x.Id == directorId);
            director.Name = directorUpdateDto.Name == default ? director.Name : directorUpdateDto.Name;
            director.Surname = directorUpdateDto.Surname == default ? director.Surname : directorUpdateDto.Surname;
            _unitOfWork.Directors.Update(director);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Director updated");
        }

        private IResult CheckIfDirectorIdDontExist(int directorId)
        {
            var movie = _unitOfWork.Directors.Get(x => x.Id == directorId);
            if (movie is null)
                return new ErrorResult("Director not found");

            return new SuccessResult();
        }
    }
}
