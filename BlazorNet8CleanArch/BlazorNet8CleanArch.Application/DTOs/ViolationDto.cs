using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Application.DTOs;

public record ViolationDto(string ViolationCategory, int TsDetailId, string ProviderName, DateTime ServiceDate, string ViolationMsg);