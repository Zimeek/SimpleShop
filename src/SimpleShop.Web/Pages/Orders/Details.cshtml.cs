using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleShop.Application.Queries.Orders;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Web.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Order = await _mediator.Send(new GetOrder.Query(id));
            if (Order is null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
