using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Queries.Orders
{
    public static class GetOrder
    {
        public record Query(string orderId) : IRequest<Order>;

        public class Handler : IRequestHandler<Query, Order>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Order> Handle(Query request, CancellationToken cancellationToken)
            {
                var order =  await _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                    .Include(o => o.Details)
                    .SingleOrDefaultAsync(o => o.Id.Equals(request.orderId));

                return order;
            }
        }
    }
}
