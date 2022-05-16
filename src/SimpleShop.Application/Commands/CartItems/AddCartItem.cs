using Ardalis.GuardClauses;
using MediatR;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Commands.CartItems
{
    public static class AddCartItem
    {
        public record Command(CartItem item) : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Guard.Against.Null(request.item, nameof(request.item));
 
                _context.CartItems.Add(request.item);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
