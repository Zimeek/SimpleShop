using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Commands.CartItems
{
    public static class UpdateCartItemQuantity
    {
        public record Command(CartItem item, int quantity) : IRequest<Unit>;

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

                if(request.item.Quantity + request.quantity <= 0)
                {
                    _context.Remove(request.item);
                }
                else
                {
                    request.item.Quantity += request.quantity;
                    _context.CartItems.Update(request.item);
                }
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
