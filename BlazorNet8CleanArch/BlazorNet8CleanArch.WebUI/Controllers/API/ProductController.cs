using BlazorNet8CleanArch.Application.Commands;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Queries.ProductQry;
using BlazorNet8CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorNet8CleanArch.WebUI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetProductListQry()));
        }

        // GET api/<ProductController>/5
        [HttpGet("{Id}")]
        public async Task<ActionResult> Get([FromRoute] GetProductByIdQry request)
        {
            return Ok(await _mediator.Send(request));
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            return Ok(await _mediator.Send(new AddEditProductCmd(productDto)));
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDto productDto)
        {
            return Ok(await _mediator.Send(new AddEditProductCmd(productDto)));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteProductCmd request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
