using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Commands.CartItems
{
    public static class UpdateCartItem
    {
        public record Command(string itemId, int quantity) : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.CartItems
                    .SingleOrDefaultAsync(i => i.Id.Equals(request.itemId));

                Guard.Against.Null(item, nameof(item));

                if(item.Quantity + request.quantity <= 0)
                {
                    _context.Remove(item);
                }
                else
                {
                    item.Quantity += request.quantity;
                    _context.CartItems.Update(item);
                }
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
