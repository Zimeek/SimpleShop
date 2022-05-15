using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleShop.Application.Commands.CartItems;
using SimpleShop.Application.Commands.Orders;
using SimpleShop.Application.Queries.Carts;
using SimpleShop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Web.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutModel(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public Domain.Entities.Cart Cart { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "First name is required.")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Last name is required.")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Address is required.")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Apartment is required.")]
            public string Apartment { get; set; }
            [Required(ErrorMessage = "City is required.")]
            public string City { get; set; }
            [Required(ErrorMessage = "Postal code is required.")]
            public string PostalCode { get; set; }
            [Required(ErrorMessage = "Phone number is required.")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "Email address is required.")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Payment method is required.")]
            public string PaymentMethod { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            Cart = await _mediator.Send(new GetCart.Query(userId));
            if (Cart.IsEmpty)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);

            Cart = await _mediator.Send(new GetCart.Query(userId));
            if (Cart.IsEmpty)
            {
                return RedirectToPage("Index");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Number = new Random().Next(1000, 4000)
            };

            var success = await _mediator.Send(new CreateOrder.Command(order));
            if (success)
            {
                var orderItems = new List<OrderItem>();
                foreach (var cartItem in Cart.Items)
                {
                    orderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = cartItem.ProductId,
                        OrderId = order.Id,
                        Quantity = cartItem.Quantity,
                        Size = cartItem.Size
                    });
                }

                var orderDetails = new OrderDetails
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = order.Id,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Address = Input.Address,
                    Apartment = Input.Apartment,
                    City = Input.City,
                    PostalCode = Input.PostalCode,
                    Phone = Input.Phone,
                    Email = Input.Email,
                    PaymentMethod = Input.PaymentMethod
                };

                await _mediator.Send(new AddOrderItems.Command(orderItems));
                await _mediator.Send(new AddOrderDetails.Command(orderDetails));
                await _mediator.Send(new ClearCart.Command(Cart.Id));
            }

            return RedirectToPage("Summary", new { orderId = order.Id });
        }
    }
}
