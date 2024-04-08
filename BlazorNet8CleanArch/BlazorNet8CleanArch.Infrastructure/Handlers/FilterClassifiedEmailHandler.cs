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
        // Tokenize the email text
        var tokens = emailText.Split(new[] { ' ', '.', ',', ';', ':', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // Convert classified words/phrases to lowercase for case-insensitive comparison
        var lowerClassifiedWords = classifiedWords.Select(word => word.ToLower()).ToHashSet();

        // Check if any of the classified words/phrases exist as whole words in the email text (case-insensitive)
        var isClassified = tokens.Any(token => lowerClassifiedWords.Contains(token.ToLower()));

        // Replace classified words/phrases with asterisks (case-insensitive)
        var censoredText = emailText;
        foreach (var word in classifiedWords)
        {
            var lowerWord = word.ToLower();
            var tokenizedWord = $@"\b{lowerWord}\b"; // Ensure whole word match using word boundary \b
            censoredText = Regex.Replace(censoredText, tokenizedWord, new string('*', word.Length), RegexOptions.IgnoreCase);
        }

        return new ClassifiedEmailFilterDto(isClassified, censoredText);
    }
}

