using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Commands.Orders
{
    public static class AddOrderItems
    {
        public record Command(List<OrderItem> orderItems) : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context.OrderItems.AddRangeAsync(request.orderItems);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
