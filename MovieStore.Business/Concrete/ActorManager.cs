using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ActorManager : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ActorManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ActorAddDto actorAddDto)
        {
            ValidationTool.Validate(new ActorAddDtoValidator(), actorAddDto);

            var actor = _mapper.Map<Actor>(actorAddDto);
            await _unitOfWork.Actors.AddAsync(actor);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Actor added");
        }

        public async Task<IResult> Delete(int actorId)
        {
            IResult result = BusinessRules.Run(CheckIfActorIdDontExist(actorId));
            if (result != null)
            {
                return result;
            }

            var actor = await _unitOfWork.Actors.GetByIdAsync(actorId);
            _unitOfWork.Actors.Remove(actor);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult();
        }

        public IDataResult<List<ActorsDto>> GetAll()
        {
            var actors = _unitOfWork.Actors.GetAll();
            return new SuccessDataResult<List<ActorsDto>>(_mapper.Map<List<ActorsDto>>(actors));
        }

        public IDataResult<ActorDetailDto> GetById(int actorId)
        {
            IResult result = BusinessRules.Run(CheckIfActorIdDontExist(actorId));
            if (result != null)
            {
                return new ErrorDataResult<ActorDetailDto>(result.Message);
            }

            var actor = _unitOfWork.Actors.Where(x=>x.Id==actorId).Include(x=>x.MovieActors).ThenInclude(x=>x.Movie).FirstOrDefault();
            ActorDetailDto vm = _mapper.Map<ActorDetailDto>(actor);
            return new SuccessDataResult<ActorDetailDto>(vm);
        }

        public async Task<IResult> Update(int actorId, ActorUpdateDto actorUpdateDto)
        {
            ValidationTool.Validate(new ActorUpdateDtoValidator(), actorUpdateDto);

            IResult result = BusinessRules.Run(CheckIfActorIdDontExist(actorId));
            if (result!=null)
            {
                return result;
            }

            var actor = await _unitOfWork.Actors.GetByIdAsync(actorId);
            actor.Name = actorUpdateDto.Name == default ? actor.Name : actorUpdateDto.Name;
            actor.Surname = actorUpdateDto.Surname == default ? actor.Surname : actorUpdateDto.Surname;
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Actor updated");
        }

        private IResult CheckIfActorIdDontExist(int actorId)
        {
            var movie = _unitOfWork.Actors.Where(x=>x.Id==actorId).FirstOrDefault();
            if (movie is null)
                return new ErrorResult("Actor not found");

            return new SuccessResult();
        }
    }
}
