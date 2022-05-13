using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Infrastructure.Database.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .HasMany<OrderItem>(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<OrderDetails>( o => o.Details)
                .WithOne(od => od.Order)
                .HasForeignKey<OrderDetails>(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
