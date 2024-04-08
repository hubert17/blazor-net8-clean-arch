using BlazorNet8CleanArch.Application.Commands;
using BlazorNet8CleanArch.Application.DTOs;
using BlazorNet8CleanArch.Application.Wrapper;
using BlazorNet8CleanArch.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Infrastructure.Handlers;

public class FilterClassifiedEmailHandler : IRequestHandler<FilterClassifiedEmailCmd, ClassifiedEmailFilterDto>
{
    public Task<ClassifiedEmailFilterDto> Handle(FilterClassifiedEmailCmd request, CancellationToken cancellationToken)
    {
        var result = FilterClassifiedEmail(request.ClassifiedWords!, request.EmailText!);

        return Task.FromResult(result);
    }

    private ClassifiedEmailFilterDto FilterClassifiedEmail(string[] classifiedWords, string emailText)
    {
        // Check if any of the classified words/phrases exist in the email text
        var isClassified = classifiedWords.Any(word => emailText.Contains(word, StringComparison.OrdinalIgnoreCase));

        // Replace classified words/phrases with asterisks
        var censoredText = emailText;
        foreach (var word in classifiedWords)
        {
            //censoredText = censoredText.Replace(word, new string('*', word.Length));
            censoredText = Regex.Replace(censoredText, word, new string('*', word.Length), RegexOptions.IgnoreCase);
        }

        return new ClassifiedEmailFilterDto(isClassified, censoredText);
    }
}

