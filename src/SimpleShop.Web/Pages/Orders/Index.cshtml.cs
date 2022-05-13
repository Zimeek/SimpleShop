using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleShop.Domain.Entities;
using SimpleShop.Application.Queries.Orders;

namespace SimpleShop.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public List<Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            Orders = await _mediator.Send(new GetOrders.Query(userId));

            return Page();
        }
    }
}
