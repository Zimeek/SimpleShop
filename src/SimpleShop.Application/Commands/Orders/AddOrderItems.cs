using Ardalis.GuardClauses;
using MediatR;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

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
                Guard.Against.NullOrEmpty(request.orderItems, nameof(request.orderItems));

                await _context.OrderItems.AddRangeAsync(request.orderItems);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
