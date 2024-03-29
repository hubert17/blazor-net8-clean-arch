﻿// <auto-generated />
using BlazorNet8CleanArch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorNet8CleanArch.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorNet8CleanArch.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureFilename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Original scent, 64 loads",
                            Name = "Tide Laundry Detergent",
                            Unit = "bottle",
                            UnitPrice = 350.00m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Size 4, 120 count",
                            Name = "Pampers Diapers",
                            Unit = "box",
                            UnitPrice = 999.00m
                        },
                        new
                        {
                            Id = 3,
                            Description = "With 1 blade cartridge",
                            Name = "Gillette Fusion Razor",
                            Unit = "pack",
                            UnitPrice = 199.75m
                        },
                        new
                        {
                            Id = 4,
                            Description = "4 bars, moisturizing cream",
                            Name = "Dove Beauty Bar",
                            Unit = "pack",
                            UnitPrice = 75.00m
                        },
                        new
                        {
                            Id = 5,
                            Description = "Real, 30 oz",
                            Name = "Hellmann's Mayonnaise",
                            Unit = "jar",
                            UnitPrice = 120.00m
                        },
                        new
                        {
                            Id = 6,
                            Description = "Phoenix scent, 4 oz",
                            Name = "Axe Body Spray",
                            Unit = "bottle",
                            UnitPrice = 190.00m
                        },
                        new
                        {
                            Id = 7,
                            Description = "Classic, 12 oz",
                            Name = "Nescafe Instant Coffee",
                            Unit = "jar",
                            UnitPrice = 170.00m
                        },
                        new
                        {
                            Id = 8,
                            Description = "Milk chocolate, 1.5 oz",
                            Name = "KitKat Chocolate Bar",
                            Unit = "bar",
                            UnitPrice = 30.00m
                        },
                        new
                        {
                            Id = 9,
                            Description = "24-pack, 16.9 oz bottles",
                            Name = "Nestle Pure Life Water",
                            Unit = "pack",
                            UnitPrice = 180.00m
                        },
                        new
                        {
                            Id = 10,
                            Description = "1 Liter bottle of Coca-Cola",
                            Name = "Coca-Cola 1 Liter",
                            Unit = "bottle",
                            UnitPrice = 30.00m
                        },
                        new
                        {
                            Id = 11,
                            Description = "12 fl oz can of Sprite",
                            Name = "Sprite 12 oz",
                            Unit = "can",
                            UnitPrice = 12.00m
                        },
                        new
                        {
                            Id = 12,
                            Description = "Orange juice with pulp, 16.9 fl oz bottle",
                            Name = "Minute Maid Pulpy Orange",
                            Unit = "bottle",
                            UnitPrice = 25.00m
                        },
                        new
                        {
                            Id = 13,
                            Description = "750ml",
                            Name = "Tanduay White Rum",
                            Unit = "bottle",
                            UnitPrice = 125.375m
                        },
                        new
                        {
                            Id = 14,
                            Description = "1000ml",
                            Name = "Red Horse Beer",
                            Unit = "bottle",
                            UnitPrice = 118.0625m
                        },
                        new
                        {
                            Id = 15,
                            Description = "1L blended premium brandy",
                            Name = "Emperador Light",
                            Unit = "bottle",
                            UnitPrice = 165.442m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
