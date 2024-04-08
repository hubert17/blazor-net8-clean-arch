using BlazorNet8CleanArch.Application.Commands;
using BlazorNet8CleanArch.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorNet8CleanArch.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedEmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassifiedEmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: ClassifiedEmailController/Create
        [HttpPost]
        public async Task<ActionResult> Create(FilterClassifiedEmailCmd request)
        {
            return Ok(await _mediator.Send(request));
        }

        
    }
}
