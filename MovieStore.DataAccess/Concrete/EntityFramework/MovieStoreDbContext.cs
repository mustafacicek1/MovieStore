using Microsoft.EntityFrameworkCore;
using MovieStore.Entities.Concrete;
using System.Reflection;

namespace MovieStore.DataAccess.Concrete.EntityFramework
{
    public class MovieStoreDbContext : DbContext
    {

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerGenre> CustomerGenres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-TTVAJ5K;Database=MovieStoreDb;Trusted_Connection=true;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
