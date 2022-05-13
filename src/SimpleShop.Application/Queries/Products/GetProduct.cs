using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Queries.Products
{
    public static class GetProduct
    {
        public record Query(string Id) : IRequest<Product>;

        public class Handler : IRequestHandler<Query, Product>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Product> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                    .Where(p => p.Id.Equals(request.Id))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return product is not null ? product : null;

            }
        }
    }
}
