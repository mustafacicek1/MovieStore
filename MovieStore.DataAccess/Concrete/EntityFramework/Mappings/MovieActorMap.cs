using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Mappings
{
    public class MovieActorMap : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.ActorId }).IsClustered(false);

            builder.HasOne(x => x.Actor).WithMany(y => y.MovieActors)
                .HasForeignKey(x => x.ActorId);

            builder.HasOne(x => x.Movie).WithMany(y => y.MovieActors)
                .HasForeignKey(x => x.MovieId);
        }
    }
}
