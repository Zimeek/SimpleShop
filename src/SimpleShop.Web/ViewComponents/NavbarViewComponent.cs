using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Queries.Carts;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;

        public NavbarViewComponent(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Cart cart = null;

            if(user is not null)
            {
                cart = await _mediator.Send(new GetCart.Query(user.Id));
            }

            return View(cart);
        }
    }
}
