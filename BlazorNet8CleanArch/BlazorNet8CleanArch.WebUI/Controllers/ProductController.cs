using BlazorNet8CleanArch.Application.Commands;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Queries.ProductQry;
using BlazorNet8CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorNet8CleanArch.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await mediator.Send(new GetProductListQry()));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await mediator.Send(new GetProductByIdQry { Id = id }));
        }

        // POST api/<ProductController>
        [HttpPost] 
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            return Ok(await mediator.Send(new AddEditProductCmd { ProductDto = productDto }));
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDto productDto)
        {
            return Ok(await mediator.Send(new AddEditProductCmd { ProductDto = productDto }));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await mediator.Send(new DeleteProductCmd { Id = id }));
        }
    }
}
