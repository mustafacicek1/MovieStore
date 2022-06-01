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
    public class ActorManager : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ActorManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IResult Add(ActorAddDto actorAddDto)
        {
            ValidationTool.Validate(new ActorAddDtoValidator(), actorAddDto);

            var actor = _mapper.Map<Actor>(actorAddDto);
            _unitOfWork.Actors.Add(actor);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Actor added");
        }

        public IResult Delete(int actorId)
        {
            IResult result = BusinessRules.Run(CheckIfActorIdDontExist(actorId));
            if (result != null)
            {
                return result;
            }

            var actor = _unitOfWork.Actors.Get(x => x.Id == actorId);
            _unitOfWork.Actors.Delete(actor);
            _unitOfWork.SaveChanges();
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

            var actor = _unitOfWork.Actors.Get(x => x.Id == actorId);
            var actorMovies = _unitOfWork.MovieActors.GetAll(x => x.ActorId == actorId,x=>x.Movie);
            ActorDetailDto vm = _mapper.Map<ActorDetailDto>(actor);
            foreach (var movie in actorMovies)
            {
                vm.Movies.Add(movie.Movie.Name);
            }
            return new SuccessDataResult<ActorDetailDto>(vm);
        }

        public IResult Update(int actorId, ActorUpdateDto actorUpdateDto)
        {
            ValidationTool.Validate(new ActorUpdateDtoValidator(), actorUpdateDto);

            IResult result = BusinessRules.Run(CheckIfActorIdDontExist(actorId));
            if (result!=null)
            {
                return result;
            }

            var actor = _unitOfWork.Actors.Get(x => x.Id == actorId);
            actor.Name = actorUpdateDto.Name == default ? actor.Name : actorUpdateDto.Name;
            actor.Surname = actorUpdateDto.Surname == default ? actor.Surname : actorUpdateDto.Surname;
            _unitOfWork.Actors.Update(actor);
            _unitOfWork.SaveChanges();
            return new SuccessResult("Actor updated");
        }

        private IResult CheckIfActorIdDontExist(int actorId)
        {
            var movie = _unitOfWork.Actors.Get(x => x.Id == actorId);
            if (movie is null)
                return new ErrorResult("Actor not found");

            return new SuccessResult();
        }
    }
}
