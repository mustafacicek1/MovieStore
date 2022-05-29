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
        private EfCustomerRepository _customerRepository;
        private EfMovieActorRepository _movieActorRepository;
        private EfActorRepository _actorRepository;
        private EfMovieRepository _movieRepository;
        private EfOrderRepository _orderRepository;
        private EfDirectorRepository _directorRepository;
        private EfCustomerGenreRepository _customerGenreRepository;
        private EfCustomerMovieRepository _customerMovieRepository;
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
