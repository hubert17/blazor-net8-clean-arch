using Xunit;
using FluentAssertions;
using BlazorNet8CleanArch.Domain.Entities;

namespace BlazorNet8CleanArch.UnitTests.Domain;

public class ProductTests
{
    [Fact]
    public void Product_Properties_Should_Be_Set_Correctly()
    {
        // Arrange
        var productId = 1;
        var productName = "Test Product";
        var productDescription = "This is a test product";
        var unitPrice = 10.99m;
        var unit = "Piece";
        var pictureFilename = "test_product.jpg";

        // Act
        var product = new Product
        {
            Id = productId,
            Name = productName,
            Description = productDescription,
            UnitPrice = unitPrice,
            Unit = unit,
            PictureFilename = pictureFilename
        };

        // Assert
        product.Id.Should().Be(productId);
        product.Name.Should().Be(productName);
        product.Description.Should().Be(productDescription);
        product.UnitPrice.Should().Be(unitPrice);
        product.Unit.Should().Be(unit);
        product.PictureFilename.Should().Be(pictureFilename);
    }

    [Fact]
    public void Product_Id_Should_Not_Be_Negative()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = -1;

        // Assert
        product.Id.Should().BeGreaterOrEqualTo(0);
    }

    [Fact]
    public void Product_UnitPrice_Should_Not_Be_Negative()
    {
        // Arrange
        var product = new Product();

        // Act
        product.UnitPrice = -10.99m;

        // Assert
        product.UnitPrice.Should().BeGreaterOrEqualTo(0);
    }

    [Fact]
    public void Product_Name_Should_Not_Be_Null_Or_Empty()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = "";

        // Assert
        product.Name.Should().NotBeNullOrEmpty();
    }
}
