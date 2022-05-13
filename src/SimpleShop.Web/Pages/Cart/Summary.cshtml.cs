using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleShop.Application.Queries.Orders;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Web.Pages.Cart
{
    public class SummaryModel : PageModel
    {
        private readonly IMediator _mediator;

        public SummaryModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Order Order { get; private set; }

        public async Task<IActionResult> OnGetAsync(string orderId)
        {
            Order = await _mediator.Send(new GetOrder.Query(orderId));

            if (Order is null)
            {
                return RedirectToPage("/Orders/Index", new { area = "" });
            }

            return Page();
        }
    }
}
