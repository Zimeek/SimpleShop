using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Application.Commands.CartItems
{
    public static class DeleteCartItem
    {
        public record Command(string itemId) : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.CartItems.SingleOrDefaultAsync(i => i.Id.Equals(request.itemId));

                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
