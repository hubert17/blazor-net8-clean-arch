using AutoMapper;
using BlazorNet8CleanArch.Application.Commands;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using BlazorNet8CleanArch.Domain.Entities;
using BlazorNet8CleanArch.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlazorNet8CleanArch.Infrastructure.Handlers.ProductHandler
{
    public class AddEditProductHandler : IRequestHandler<AddEditProductCmd, Result<int>>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public AddEditProductHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _db = appDbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditProductCmd request, CancellationToken cancellationToken)
        {
            var exists = await _db.Products.Where(x => x.Id != request.ProductDto!.Id).AnyAsync(x => x.Name!.ToLower() == request.ProductDto!.Name!.ToLower());
            if (exists)
                return await Result<int>.FailAsync("Product already exists");

            var product = _mapper.Map<Product>(request.ProductDto);

            if (request.ProductDto!.Id > 0)
            {
                // var product = await _db.Products.FindAsync(request.ProductDto!.Id);
                var found = await _db.Products.AnyAsync(x => x.Id == request.ProductDto.Id);
                _db.Entry(product).State = EntityState.Modified;
                await _db.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(product!.Id, "Updated");
            }
            else
            {                
                _db.Products.Add(product);
                await _db.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(product.Id, "Added");
            }

        }
    }
}
