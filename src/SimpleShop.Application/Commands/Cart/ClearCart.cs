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
                var cart = await _context.Carts
                    .Include(c => c.Items)
                    .SingleOrDefaultAsync(c => c.Id.Equals(request.cartId));

                Guard.Against.Null(cart, nameof(cart));

                if(!cart.IsEmpty)
                {
                    _context.RemoveRange(cart.Items);
                    await _context.SaveChangesAsync();
                }

                return Unit.Value;
            }
        }
    }
}
