using MovieStore.Core.Utilities.Results;
using MovieStore.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Abstract
{
    public interface IDirectorService
    {
        Task<IResult> Add(DirectorAddDto directorAddDto);
        Task<IResult> Delete(int directorId);
        Task<IResult> Update(int directorId, DirectorUpdateDto directorUpdateDto);
        IDataResult<List<DirectorsDto>> GetAll();
        IDataResult<DirectorDetailDto> GetById(int directorId);
    }
}
