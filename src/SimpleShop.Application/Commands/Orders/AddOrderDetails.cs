using MediatR;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Commands.Orders
{
    public static class AddOrderDetails
    {
        public record Command(SimpleShop.Domain.Entities.OrderDetails orderDetails) : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.OrderDetails.Add(request.orderDetails);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
