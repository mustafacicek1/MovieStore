using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Entities.Concrete;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price).HasColumnType("money");
            builder.Property(x => x.OrderDate).HasColumnType("datetime");

            builder.HasOne(x => x.Customer).WithMany(y => y.Orders)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
