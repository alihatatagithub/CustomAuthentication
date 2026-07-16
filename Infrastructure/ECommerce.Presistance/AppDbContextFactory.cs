using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Presistance
{
    // Design-time factory to ensure EF tools can create AppDbContext with the correct connection string
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Try current directory (usually the startup project's folder) and a known sibling path
            var basePath = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine("..", "Presentation", "ECommerce.Api", "appsettings.json"), optional: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var conn = configuration.GetConnectionString("DefaultConnection")
                       ?? configuration["ConnectionStrings:DefaultConnection"];

            if (string.IsNullOrWhiteSpace(conn))
            {
                throw new InvalidOperationException("Could not find a valid connection string for 'DefaultConnection'. Make sure appsettings.json or environment variables are available to the EF tools.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(conn, npgsqlOptions =>
            {
                // ensure migrations go to the same assembly as the DbContext
                npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
