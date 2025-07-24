using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Core.Entities;
using CustomerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider, string csvFilePath)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

            try
            {
                // Ensure database is created
                await dbContext.Database.MigrateAsync();

                // Check if customers already exist
                if (await dbContext.Customers.AnyAsync())
                {
                    logger.LogInformation("Data already seeded.");
                    return;
                }

                // Read CSV file
                if (!File.Exists(csvFilePath))
                {
                    logger.LogWarning("CSV file not found at path: {FilePath}", csvFilePath);
                    return;
                }

                var lines = await File.ReadAllLinesAsync(csvFilePath);
                
                // Skip header row
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    
                    if (values.Length < 16)
                    {
                        logger.LogWarning("Invalid CSV row: {Row}", line);
                        continue;
                    }

                    var customer = new Customer
                    {
                        CustomerId = values[0],
                        FirstName = values[1],
                        LastName = values[2],
                        Email = values[3],
                        PhoneNumber = values[4],
                        StreetAddress = values[5],
                        City = values[6],
                        StateProvince = values[7],
                        PostalCode = values[8],
                        Country = values[9],
                        CompanyName = string.IsNullOrEmpty(values[10]) ? null : values[10],
                        CustomerType = values[11],
                        RegistrationDate = DateTime.Parse(values[12], CultureInfo.InvariantCulture),
                        PaymentMethod = values[13],
                        Notes = string.IsNullOrEmpty(values[14]) ? null : values[14],
                        Status = values[15]
                    };

                    dbContext.Customers.Add(customer);
                }

                await dbContext.SaveChangesAsync();
                logger.LogInformation("Data seeded successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
