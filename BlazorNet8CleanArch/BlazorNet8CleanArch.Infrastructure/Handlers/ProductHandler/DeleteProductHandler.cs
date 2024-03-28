using AutoMapper;
using BlazorNet8CleanArch.Application.Commands;
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

namespace BlazorNet8CleanArch.Infrastructure.Handlers.ProductHandler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCmd, Result<int>>
    {
        private readonly AppDbContext _db;

        public DeleteProductHandler(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        public async Task<Result<int>> Handle(DeleteProductCmd request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(request.Id);
            if(product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(product.Id, "Deleted");
            }

            return await Result<int>.FailAsync("Not Found");
        }
    }
}
