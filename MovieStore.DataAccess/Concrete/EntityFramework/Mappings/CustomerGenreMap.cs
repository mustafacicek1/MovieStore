using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CustomerGenreMap : IEntityTypeConfiguration<CustomerGenre>
    {
        public void Configure(EntityTypeBuilder<CustomerGenre> builder)
        {
            builder.HasKey(x => new { x.GenreId, x.CustomerId }).IsClustered(false);

            builder.HasOne(x => x.Genre).WithMany(y => y.CustomerGenres)
                .HasForeignKey(x => x.GenreId);

            builder.HasOne(x => x.Customer).WithMany(y => y.CustomerGenres)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
