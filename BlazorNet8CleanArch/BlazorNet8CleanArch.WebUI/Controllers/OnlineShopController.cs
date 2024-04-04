using BlazorNet8CleanArch.Application.Queries.ProductQry;
using BlazorNet8CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorNet8CleanArch.WebUI.Controllers
{
    [Route("[controller]")]
    public class OnlineShopController : Controller
    {
        private readonly IMediator _mediator;

        public OnlineShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductListQry());
            return View(products.Data);
        }

        [HttpGet("{Id}/{Name}")]
        public async Task<ActionResult> Details([FromRoute] GetProductByIdQry request)
        {
            var product = await _mediator.Send(request);
            return View(product.Data);
        }
    }
}
