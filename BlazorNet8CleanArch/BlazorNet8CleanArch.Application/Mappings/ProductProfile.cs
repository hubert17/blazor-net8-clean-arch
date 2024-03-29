using AutoMapper;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Domain.Entities;

namespace BlazorNet8CleanArch.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
