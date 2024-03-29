using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Queries.ProductQry;

public record GetProductByIdQry(int Id) : IRequest<Result<ProductDto>>;
