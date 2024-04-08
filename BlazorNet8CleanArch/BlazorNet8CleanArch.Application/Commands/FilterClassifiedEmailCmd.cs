using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using MediatR;

namespace BlazorNet8CleanArch.Application.Commands;


public record FilterClassifiedEmailCmd(string[] ClassifiedWords, string EmailText) : IRequest<ClassifiedEmailFilterDto>;
