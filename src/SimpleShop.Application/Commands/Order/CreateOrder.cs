using Ardalis.GuardClauses;
using MediatR;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Commands.Orders
{
    public static class CreateOrder
    {
        public record Command(Order order) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                Guard.Against.Null(request.order, nameof(request.order));

                _context.Orders.Add(request.order);
                var success = await _context.SaveChangesAsync();

                return success > 0;
            }
        }
    }
}
