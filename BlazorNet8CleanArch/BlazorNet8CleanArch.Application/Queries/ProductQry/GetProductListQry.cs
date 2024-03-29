using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Queries.ProductQry;

public record GetProductListQry : IRequest<Result<List<ProductDto>>>;
