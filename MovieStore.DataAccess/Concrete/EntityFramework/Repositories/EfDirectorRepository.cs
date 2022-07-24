using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using System.Linq;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfDirectorRepository : GenericRepository<Director>, IDirectorRepository
    {
        public EfDirectorRepository(MovieStoreDbContext context):base(context)
        {

        }

        public Director GetDirectorDetails(int directorId)
        {
            return _context.Directors.Where(x => x.Id == directorId).Include(x => x.Movies).FirstOrDefault();
        }
    }
}
