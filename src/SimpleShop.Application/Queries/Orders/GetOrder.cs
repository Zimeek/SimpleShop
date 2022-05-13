using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return await _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                    .Include(o => o.Details)
                    .FirstOrDefaultAsync(o => o.Id.Equals(request.orderId));
            }
        }
    }
}
