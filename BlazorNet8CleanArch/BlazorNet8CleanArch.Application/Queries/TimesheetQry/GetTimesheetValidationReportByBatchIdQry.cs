using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Queries.TimesheetQry;

public record GetTimesheetValidationReportByBatchIdQry(int BatchId) : IRequest<Result<ProductDto>>;