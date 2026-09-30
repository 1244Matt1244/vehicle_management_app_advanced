using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IDbConnectionFactory));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddSingleton<IDbConnectionFactory, FakeDbConnectionFactory>();
        });
    }
}
