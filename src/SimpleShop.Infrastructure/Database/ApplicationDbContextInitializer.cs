using SimpleShop.Domain.Entities;

namespace SimpleShop.Infrastructure.Database
{
    public static class ApplicationDbContextInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if(!context.Products.Any())
            {
                context.AddRange(new[]
                {
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Superstar Shoes", ImagePath = "adidas1.webp", Price = 20 },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "ZX 2K Boost 2.0 Shoes", ImagePath = "adidas2.webp", Price = 20 },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Ultra Boost 1.0 DNA Shoes", ImagePath = "adidas3.jpg", Price = 20 },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Forum Low Shoes", ImagePath = "adidas4.webp", Price = 20 },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Ozelia Shoes", ImagePath = "adidas5.webp", Price = 20 },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Advantage Base Shoes", ImagePath = "adidas6.webp", Price = 20 },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Ultra Boost DNA 2.0 Shoes", ImagePath = "adidas9.webp", Price = 20 },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "OZWEEGO Celox Shoes", ImagePath = "adidas10.webp", Price = 20 }
                });
            }

            context.SaveChanges();
        } 
    }
}
