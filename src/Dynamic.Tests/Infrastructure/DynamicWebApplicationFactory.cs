using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dynamic.Application.Ports.Out.Repositories;

namespace Dynamic.Tests.Infrastructure
{
    // This class allows testing with the Web project's Program.cs
    public class DynamicWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            // You can configure services for testing here if needed
            builder.ConfigureServices(services =>
            {
                // Replace the real repository with our mock for testing
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IEntityTypeRepository));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddScoped<IEntityTypeRepository, MockEntityTypeRepository>();

                // Add any other test-specific services here
            });
        }
    }
}
