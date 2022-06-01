using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(25);
            builder.Property(x => x.Surname).HasMaxLength(25);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Password).HasMaxLength(12);
        }
    }
}
