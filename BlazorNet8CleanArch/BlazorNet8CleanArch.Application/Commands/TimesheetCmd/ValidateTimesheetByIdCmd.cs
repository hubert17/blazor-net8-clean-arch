using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Commands.TimesheetCmd;

public record ValidateTimesheetByIdCmd(int Id) : IRequest<Result<List<ViolationDto>>>;
