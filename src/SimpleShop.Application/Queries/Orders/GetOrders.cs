using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Queries.Orders
{
    public static class GetOrders
    {
        public record Query(string userId) : IRequest<List<Order>>;

        public class Handler : IRequestHandler<Query, List<Order>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Order>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders
                    .Where(o => o.UserId.Equals(request.userId))
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                    .Include(o => o.Details)
                    .ToListAsync();

                return orders;
            }
        }
    }
}
