﻿using BlazorNet8CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(GetProducts());
        }

        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                // Procter & Gamble products
                new Product { Id = 1, Name = "Tide Laundry Detergent", UnitPrice = 350.00m, Description = "Original scent, 64 loads", Unit = "bottle" },
                new Product { Id = 2, Name = "Pampers Diapers", UnitPrice = 999.00m, Description = "Size 4, 120 count", Unit = "box" },
                new Product { Id = 3, Name = "Gillette Fusion Razor", UnitPrice = 199.75m, Description = "With 1 blade cartridge", Unit = "pack" },

                // Unilever products
                new Product { Id = 4, Name = "Dove Beauty Bar", UnitPrice = 75.00m, Description = "4 bars, moisturizing cream", Unit = "pack" },
                new Product { Id = 5, Name = "Hellmann's Mayonnaise", UnitPrice = 120.00m, Description = "Real, 30 oz", Unit = "jar" },
                new Product { Id = 6, Name = "Axe Body Spray", UnitPrice = 190.00m, Description = "Phoenix scent, 4 oz", Unit = "bottle" },

                // Nestle products
                new Product { Id = 7, Name = "Nescafe Instant Coffee", UnitPrice = 170.00m, Description = "Classic, 12 oz", Unit = "jar" },
                new Product { Id = 8, Name = "KitKat Chocolate Bar", UnitPrice = 30.00m, Description = "Milk chocolate, 1.5 oz", Unit = "bar" },
                new Product { Id = 9, Name = "Nestle Pure Life Water", UnitPrice = 180.00m, Description = "24-pack, 16.9 oz bottles", Unit = "pack" },

                // Coca-Cola products
                new Product { Id = 10, Name = "Coca-Cola 1 Liter", UnitPrice = 30.00m, Description = "1 Liter bottle of Coca-Cola", Unit = "bottle" },
                new Product { Id = 11, Name = "Sprite 12 oz", UnitPrice = 12.00m, Description = "12 fl oz can of Sprite", Unit = "can" },
                new Product { Id = 12, Name = "Minute Maid Pulpy Orange", UnitPrice = 25.00m, Description = "Orange juice with pulp, 16.9 fl oz bottle", Unit = "bottle" },

                // Alcoholic beverages
                new Product { Id = 13, Name = "Tanduay White Rum", UnitPrice = 125.375m, Description = "750ml", Unit = "bottle" },
                new Product { Id = 14, Name = "Red Horse Beer", UnitPrice = 118.0625m, Description = "1000ml", Unit = "bottle" },
                new Product { Id = 15, Name = "Emperador Light", UnitPrice = 165.442m, Description = "1L blended premium brandy", Unit = "bottle" }
            };
        }
    }
}
