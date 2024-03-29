namespace BlazorNet8CleanArch.Tests.Products;

using BlazorNet8CleanArch.Application.Queries.ProductQry;
using BlazorNet8CleanArch.Domain.Entities;
using static Testing;

public class GetProductListHandlerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        var query = new GetProductListQry();

        var result = await SendAsync(query);

        result.Data.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await AddAsync(new Product { Name = "Tide Laundry Detergent", UnitPrice = 350.00m, Description = "Original scent, 64 loads", Unit = "bottle" });
        await AddAsync(new Product { Name = "Pampers Diapers", UnitPrice = 999.00m, Description = "Size 4, 120 count", Unit = "box" });

        var query = new GetProductListQry();

        var result = await SendAsync(query);

        result.Data.Should().HaveCount(2);
    }

}