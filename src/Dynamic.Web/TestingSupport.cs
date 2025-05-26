using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamic.Web
{
    // This public Program class exists solely to enable WebApplicationFactory in tests
    // The actual program logic is in Program.cs using top-level statements
    public class Program
    {
        // Empty constructor required by WebApplicationFactory
        public Program() { }

        // Static entry point for WebApplicationFactory
        public static void Main(string[] args)
        {
            // This is never called directly, but WebApplicationFactory needs it
        }

        // This method is required by WebApplicationFactory
        // The test framework will use this to create a test server
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    // Simple startup class to satisfy the CreateHostBuilder method
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // This is just a placeholder, actual configuration is in Program.cs
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // This is just a placeholder, actual configuration is in Program.cs
        }
    }
}
