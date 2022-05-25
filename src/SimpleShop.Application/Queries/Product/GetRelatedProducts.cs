using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Queries.Products
{
    public static class GetRelatedProducts
    {
        public record Query(Product product) : IRequest<IEnumerable<Product>>;

        public class Handler : IRequestHandler<Query, IEnumerable<Product>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Product>> Handle(Query request, CancellationToken cancellationToken)
            {
                var relatedProducts = await _context.Products
                    .Where(p => p.Id != request.product.Id)
                    .OrderBy(r => Guid.NewGuid())
                    .Take(4)
                    .ToListAsync();

                return relatedProducts;
            }
        }
    }
}
