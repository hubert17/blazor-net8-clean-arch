using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using BlazorNet8CleanArch.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Application.Queries.ProductQry
{
    public class GetProductByIdQry() : IRequest<Result<ProductDto>>
    {
        public int Id { get; set; } 
    }
}
