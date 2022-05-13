using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Queries.CartItems
{
    public static class GetCartItem
    {
        public record Query(string id) : IRequest<CartItem>;

        public class Handler : IRequestHandler<Query, CartItem>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CartItem> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.CartItems
                    .Include(p => p.Product)
                    .FirstOrDefaultAsync(p => p.Id.Equals(request.id));

                return product;
            }
        }
    }
}
