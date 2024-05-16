using Fintastic_API.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;


namespace Fintastic_API.Tests.Helpers
{
    public class MockDb : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<DataContext>));

                services.AddDbContext<DataContext>(options =>
                    options.UseInMemoryDatabase("FinanceDatabase", root));
            });

            return base.CreateHost(builder);
        }
    }
}
