using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Application.Commands
{
    public class DeleteProductCmd : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
}
