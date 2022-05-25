using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Queries.Carts
{
    public static class GetCart
    {
        public record Query(string userId) : IRequest<Cart>;

        public class Handler : IRequestHandler<Query, Cart>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Cart> Handle(Query request, CancellationToken cancellationToken)
            {
                var cart = await _context.Carts
                    .AsNoTracking()
                    .Include(c => c.Items)
                    .ThenInclude(p => p.Product)
                    .SingleOrDefaultAsync(c => c.UserId.Equals(request.userId));

                return cart;
            }
        }
    }
}
