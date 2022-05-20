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
    public static class GetProducts
    {
        public record Query() : IRequest<List<Product>>;

        public class Handler : IRequestHandler<Query, List<Product>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Product>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await _context.Products
                    .AsNoTracking()
                    .ToListAsync();

                return products;
            }
        }
    }
}
