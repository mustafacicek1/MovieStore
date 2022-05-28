using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Mappings
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(25);
            builder.Property(x => x.Price).HasColumnType("money");

            builder.HasMany(x => x.Orders).WithOne(y => y.Movie)
                .HasForeignKey(y => y.MovieId);
        }
    }
}
