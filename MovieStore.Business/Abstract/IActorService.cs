using MovieStore.Core.Utilities.Results;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Abstract
{
    public interface IActorService
    {
        Task<IResult> Add(ActorAddDto actorAddDto);
        Task<IResult> Delete(int actorId);
        Task<IResult> Update(int actorId,ActorUpdateDto actorUpdateDto);
        IDataResult<List<ActorsDto>> GetAll();
        IDataResult<ActorDetailDto> GetById(int actorId);
    }
}
