using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleShop.Application.Queries.Products;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _mediator.Send(new GetProducts.Query());
            
            return Page();
        }
    }
}