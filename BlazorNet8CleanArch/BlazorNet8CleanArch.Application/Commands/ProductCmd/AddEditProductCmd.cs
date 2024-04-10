using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Commands.ProductCmd;

public record AddEditProductCmd(ProductDto? ProductDto) : IRequest<Result<int>>;
