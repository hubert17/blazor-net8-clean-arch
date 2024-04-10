using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Commands.TimesheetCmd;

public record ValidateTimesheetByIdsCmd(int[] Ids = null!, int[] IgnoreIds = null!) : IRequest<Result<List<ViolationDto>>>;
