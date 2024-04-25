using AutoMapper;
using Azure.Core;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Queries.ProductQry;
using BlazorNet8CleanArch.Application.Wrapper;
using BlazorNet8CleanArch.Domain.Entities;
using BlazorNet8CleanArch.Infrastructure.Data;
using MediatR;

namespace BlazorNet8CleanArch.Infrastructure.Handlers.ProductHandler
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQry, Result<ProductDto>>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _db = appDbContext;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQry request, CancellationToken cancellationToken)
        {
            var product = await CacheManager.GetOrSetAsync($"{nameof(Product)}_{request.Id}", () => GetById(request.Id), DateTimeOffset.UtcNow.AddMinutes(1));
            if (product != null)
            {
                var data = _mapper.Map<ProductDto>(product);
                return await Result<ProductDto>.SuccessAsync(data);
            }

            return await Result<ProductDto>.FailAsync("Not Found");
        }

        private async Task<Product> GetById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if(product != null)
                product.Name = $"{product.Name} [{DateTime.Now}]";

            return product!;
        }
    }
}
