using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Commands.Carts
{
    public static class CreateCart
    {
        public record Command(string userId) : IRequest<Unit>;

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
                    .SingleOrDefaultAsync(c => c.UserId.Equals(request.userId));
                
                if (cart is null)
                {
                    cart = new Cart
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = request.userId
                    };
                    
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                return Unit.Value;
            }
        }
    }
}
