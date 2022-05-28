using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CustomerMovieMap : IEntityTypeConfiguration<CustomerMovie>
    {
        public void Configure(EntityTypeBuilder<CustomerMovie> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.CustomerId }).IsClustered(false);

            builder.HasOne(x => x.Movie).WithMany(y => y.CustomerMovies)
                .HasForeignKey(x => x.MovieId);

            builder.HasOne(x => x.Customer).WithMany(y => y.CustomerMovies)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
