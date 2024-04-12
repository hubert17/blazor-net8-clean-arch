using BlazorNet8CleanArch.Application.Commands.ProductCmd;
using BlazorNet8CleanArch.Application.Commands.TimesheetCmd;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Queries.TimesheetQry;
using BlazorNet8CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorNet8CleanArch.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimesheetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Timesheet is read from the database by TimesheetId
        /// </summary>
        // GET api/<TimesheetController>/52457
        [ProducesResponseType(typeof(TimesheetDto), 200)]
        [HttpGet("{Id}")]
        public async Task<ActionResult> Get([FromRoute] GetTimesheetByIdQry request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Create new timesheet for later validation but flag as Draft. This returns an Id.
        /// </summary>
        // Post api/<TimesheetController>/validate
        [HttpPost("validate")]
        public async Task<ActionResult> Validate([FromBody] ValidateTimesheetCmd request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Timesheet is read from the database by TimesheetId then validated
        /// </summary>
        // Post api/<TimesheetController>/validate/52457
        [ProducesResponseType(typeof(ViolationDto), 200)]
        [HttpPost("validate/{Id}")]
        //[HttpPost("{Id}/validate")]        
        public async Task<ActionResult> Validate([FromRoute] ValidateTimesheetByIdCmd request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Timesheet is read from the database by TimesheetId then validate specifc timesheet entry
        /// </summary>
        // Post api/<TimesheetController>/validate/52457
        [ProducesResponseType(typeof(ViolationDto), 200)]
        [HttpPost("validate/{Id}/timesheetdetail/{EntryId}")]     
        public async Task<ActionResult> ValidateTimesheetEntry([FromRoute] ValidateTimesheetByIdCmd request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Create new list of timesheets for later validation but flag as Draft. This returns BatchId.
        /// </summary>
        // Post api/<TimesheetController>/validate/batch
        [HttpPost("validate/batch")]
        public async Task<ActionResult> ValidateBatch([FromBody] ValidateTimesheetBatchCmd request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// List of Timesheets is read from the database by BatchId then validated
        /// </summary>
        // Post api/<TimesheetController>/validate/batch/5236
        [ProducesResponseType(typeof(List<ViolationDto>), 200)]
        [HttpPost("validate/batch/{BatchId}")]     
        public async Task<ActionResult> ValidateByBatchId([FromRoute] ValidateTimesheetByBatchIdCmd request)
        {
            return Ok(await _mediator.Send(request));
        }

        // GET api/<TimesheetController>/validationreport/5236
        [HttpGet("validationreport/{BatchId}")]
        public async Task<ActionResult> GetValidationReport([FromRoute] GetTimesheetByIdQry request)
        {
            return Ok(await _mediator.Send(request));
        }

    }
}
