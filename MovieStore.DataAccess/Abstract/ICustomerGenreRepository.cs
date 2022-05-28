using MovieStore.Core.DataAccess;
using MovieStore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Abstract
{
    public interface ICustomerGenreRepository : IEntityRepository<CustomerGenre>
    {
    }
}
