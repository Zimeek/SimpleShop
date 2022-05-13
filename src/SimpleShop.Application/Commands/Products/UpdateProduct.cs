using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Commands.Products
{
    public static class UpdateProduct
    {
        public record Command(Product product) : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == request.product.Id);

                if(product is not null)
                {
                    product.Name = request.product.Name;
                    product.Price = request.product.Price;
                    //TODO: ADD DESCRIPTION

                    await _context.SaveChangesAsync();
                }

                return Unit.Value;
            }
        }
    }
}
