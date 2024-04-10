using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Queries.TimesheetQry;

public record GetTimesheetByIdQry(int Id) : IRequest<Result<ProductDto>>;