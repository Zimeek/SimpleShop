using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleShop.Application.Queries.Products;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Web.Pages.Products
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

        public Product Product { get; set; }
        [BindProperty]
        public int Size { get; set; }
        public IEnumerable<Product> RelatedProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Product = await _mediator.Send(new GetProduct.Query(id));
            if(Product is null)
            {
                return RedirectToPage("/Index", new { area = "" });
            }
            RelatedProducts = await _mediator.Send(new GetRelatedProducts.Query(Product));

            return Page();
        }
    }
}
