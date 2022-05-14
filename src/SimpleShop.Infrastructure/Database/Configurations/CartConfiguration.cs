using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Infrastructure.Database.Configurations
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .Ignore(c => c.IsEmpty)
                .HasKey(c => c.Id);

            builder
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
