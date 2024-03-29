using System.Data.Common;

namespace BlazorNet8CleanArch.Tests;

public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}