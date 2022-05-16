using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleShop.Application.Commands.CartItems;
using SimpleShop.Application.Queries.Carts;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Web.Pages.Cart
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

        public Domain.Entities.Cart Cart { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            Cart = await _mediator.Send(new GetCart.Query(userId));

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteItemAsync(string itemId)
        {
            var userId = _userManager.GetUserId(User);
            Cart = await _mediator.Send(new GetCart.Query(userId));

            var item = Cart.Items.SingleOrDefault(i => i.Id.Equals(itemId));
            if (item is not null)
            {
                await _mediator.Send(new DeleteCartItem.Command(item.Id));
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostUpdateItemAsync(string itemId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            Cart = await _mediator.Send(new GetCart.Query(userId));

            var item = Cart.Items.SingleOrDefault(i => i.Id.Equals(itemId));
            if (item is not null)
            {
                await _mediator.Send(new UpdateCartItem.Command(item.Id, quantity));
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAddItemAsync(string productId, int size)
        {
            var userId = _userManager.GetUserId(User);
            Cart = await _mediator.Send(new GetCart.Query(userId));

            var item = Cart.Items.SingleOrDefault(i => i.ProductId.Equals(productId) && i.Size.Equals(size));
            if (item is not null)
            {
                await _mediator.Send(new UpdateCartItem.Command(item.Id, 1)); //w tej metodzie wywala errora
            }
            else
            {
                item = new CartItem
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = productId,
                    CartId = Cart.Id,
                    Size = size,
                    Quantity = 1
                };
                await _mediator.Send(new AddCartItem.Command(item));
            }

            return RedirectToPage("Index");
        }
    }
}
