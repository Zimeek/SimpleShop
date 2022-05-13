using MediatR;
using SimpleShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Queries.Carts
{
    public static class GetCartTotal //WYJEBAC TO
    {
        public record Query(Cart cart) : IRequest<decimal>;

        public class Handler : IRequestHandler<Query, decimal>
        {
            public async Task<decimal> Handle(Query request, CancellationToken cancellationToken)
            {
                var total = 0m;
                
                if(request.cart.Items.Any())
                {
                    foreach (var item in request.cart.Items)
                        total += item.Product.Price * item.Quantity;
                }
                return total;
            }
        }
    }
}
