using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Commands.Carts
{
    public static class ClearCart
    {
        public record Command(string cartId) : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var cartItems = await _context.CartItems
                    .Where(ci => ci.CartId.Equals(request.cartId))
                    .ToListAsync();

                Guard.Against.NullOrEmpty(cartItems, nameof(cartItems));

                _context.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
