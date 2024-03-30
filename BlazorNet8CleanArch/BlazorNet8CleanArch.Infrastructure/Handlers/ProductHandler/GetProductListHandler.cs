﻿using AutoMapper;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Queries.ProductQry;
using BlazorNet8CleanArch.Application.Wrapper;
using BlazorNet8CleanArch.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorNet8CleanArch.Infrastructure.Handlers.ProductHandler
{
    public class GetProductListHandler : IRequestHandler<GetProductListQry, Result<List<ProductDto>>>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public GetProductListHandler(AppDbContext appDbContext, IMapper mapper)
        {
             _db = appDbContext;
            _mapper = mapper;
        }
          
        public async Task<Result<List<ProductDto>>> Handle(GetProductListQry request, CancellationToken cancellationToken)
        {
            var products = await _db.Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            var data = _mapper.Map<List<ProductDto>>(products);

            return await Result<List<ProductDto>>.SuccessAsync(data);
        }
    }
}
