using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.DataSeeding
{
	public static class DataContextSeed
	{
		public static async Task SeedDataAsync(DataContext context)
		{
            // Read Data from files
            // convert data to C# objects
            // Insert data into DB


            if (!context.Set<ProductBrand>().Any())
            {
				// Read Data from files
				var strings = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Data\DataSeeding\brands.json");

				// convert data to C# objects
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(strings);

				// Insert data into DB
				if (brands is not null && brands.Any())
                {
                    await context.Set<ProductBrand>().AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }

			if (!context.Set<ProductType>().Any())
			{
				var strings = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Data\DataSeeding\types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(strings);
				if (types is not null && types.Any())
				{
					await context.Set<ProductType>().AddRangeAsync(types);
					await context.SaveChangesAsync();
				}
			}

			if (!context.Set<Product>().Any())
			{
				var strings = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Data\DataSeeding\products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(strings);
				if (products is not null && products.Any())
				{
					await context.Set<Product>().AddRangeAsync(products);
					await context.SaveChangesAsync();
				}
			}
		}
	}
}
