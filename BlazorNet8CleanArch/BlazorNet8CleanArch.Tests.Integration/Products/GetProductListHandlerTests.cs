namespace BlazorNet8CleanArch.IntegrationTests.Products;

using BlazorNet8CleanArch.Application.Queries.ProductQry;
using BlazorNet8CleanArch.Domain.Entities;
using BlazorNet8CleanArch.Infrastructure.Data;
using FluentAssertions;

public class GetProductListHandlerTests : Testing, IDisposable
{
    [Fact]
    public async Task ProductsShouldNotBeEmpty()
    {
        foreach (var product in DatabaseSeeder.GetProducts())
        {
            await AddAsync(product);
        }

        var query = new GetProductListQry();

        var result = await SendAsync(query);

        result.Data.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ProductsShouldHaveTwoMoreItems()
    {
        await AddAsync(new Product { Name = "Tide Laundry Detergent", UnitPrice = 350.00m, Description = "Original scent, 64 loads", Unit = "bottle" });
        await AddAsync(new Product { Name = "Pampers Diapers", UnitPrice = 999.00m, Description = "Size 4, 120 count", Unit = "box" });

        var query = new GetProductListQry();

        var result = await SendAsync(query);

        result.Data.Should().HaveCount(17);
    }

}