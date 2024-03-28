using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using BlazorNet8CleanArch.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Application.Commands
{
    public class AddEditProductCmd : IRequest<Result<int>>
    {
        public ProductDto? ProductDto { get; set; }
    }
}
