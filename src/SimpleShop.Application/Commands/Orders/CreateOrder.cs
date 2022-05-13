using MediatR;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Commands.Orders
{
    public static class CreateOrder
    {
        public record Command(Order order) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if(request.order is null)
                {
                    return false;
                }

                _context.Orders.Add(request.order);

                var successfull = await _context.SaveChangesAsync();

                return successfull > 0;
            }
        }
    }
}
