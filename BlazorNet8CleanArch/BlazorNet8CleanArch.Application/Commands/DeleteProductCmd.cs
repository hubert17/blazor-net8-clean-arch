using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Commands;

public record DeleteProductCmd(int Id) : IRequest<Result<int>>;
