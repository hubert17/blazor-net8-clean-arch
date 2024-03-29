using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorNet8CleanArch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureFilename = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "PictureFilename", "Unit", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Original scent, 64 loads", "Tide Laundry Detergent", null, "bottle", 350.00m },
                    { 2, "Size 4, 120 count", "Pampers Diapers", null, "box", 999.00m },
                    { 3, "With 1 blade cartridge", "Gillette Fusion Razor", null, "pack", 199.75m },
                    { 4, "4 bars, moisturizing cream", "Dove Beauty Bar", null, "pack", 75.00m },
                    { 5, "Real, 30 oz", "Hellmann's Mayonnaise", null, "jar", 120.00m },
                    { 6, "Phoenix scent, 4 oz", "Axe Body Spray", null, "bottle", 190.00m },
                    { 7, "Classic, 12 oz", "Nescafe Instant Coffee", null, "jar", 170.00m },
                    { 8, "Milk chocolate, 1.5 oz", "KitKat Chocolate Bar", null, "bar", 30.00m },
                    { 9, "24-pack, 16.9 oz bottles", "Nestle Pure Life Water", null, "pack", 180.00m },
                    { 10, "1 Liter bottle of Coca-Cola", "Coca-Cola 1 Liter", null, "bottle", 30.00m },
                    { 11, "12 fl oz can of Sprite", "Sprite 12 oz", null, "can", 12.00m },
                    { 12, "Orange juice with pulp, 16.9 fl oz bottle", "Minute Maid Pulpy Orange", null, "bottle", 25.00m },
                    { 13, "750ml", "Tanduay White Rum", null, "bottle", 125.375m },
                    { 14, "1000ml", "Red Horse Beer", null, "bottle", 118.0625m },
                    { 15, "1L blended premium brandy", "Emperador Light", null, "bottle", 165.442m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
