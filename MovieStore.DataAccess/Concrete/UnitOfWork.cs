using MovieStore.DataAccess.Abstract;
using MovieStore.DataAccess.Concrete.EntityFramework;
using MovieStore.DataAccess.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieStoreDbContext _context;
        private readonly EfCustomerRepository _customerRepository;
        private readonly EfMovieActorRepository _movieActorRepository;
        private readonly EfActorRepository _actorRepository;
        private readonly EfMovieRepository _movieRepository;
        private readonly EfOrderRepository _orderRepository;
        private readonly EfDirectorRepository _directorRepository;
        private readonly EfCustomerGenreRepository _customerGenreRepository;
        private readonly EfCustomerMovieRepository _customerMovieRepository;
        public UnitOfWork(MovieStoreDbContext context)
        {
            _context = context;
        }

        public IActorRepository Actors => _actorRepository ?? new EfActorRepository(_context);

        public ICustomerGenreRepository CustomerGenres => _customerGenreRepository ?? new EfCustomerGenreRepository(_context);

        public ICustomerMovieRepository CustomerMovies => _customerMovieRepository ?? new EfCustomerMovieRepository(_context);

        public ICustomerRepository Customers => _customerRepository ?? new EfCustomerRepository(_context);

        public IDirectorRepository Directors => _directorRepository ?? new EfDirectorRepository(_context);

        public IMovieActorRepository MovieActors => _movieActorRepository ?? new EfMovieActorRepository(_context);

        public IMovieRepository Movies => _movieRepository ?? new EfMovieRepository(_context);

        public IOrderRepository Orders => _orderRepository ?? new EfOrderRepository(_context);

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
