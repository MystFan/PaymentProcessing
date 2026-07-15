using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using PaymentProcessing.DataAccess;

namespace PaymentProcessing.IntegrationTests
{
    public class EndpointTest : PlaywrightTest, IAsyncLifetime
    {
        private static string? API_URL = Environment.GetEnvironmentVariable("API_URL");
        private static string? DATABASE_CONNECTION = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

        protected EFContext Context { get; set; } = null!;

        protected IAPIRequestContext Request { get; set; } = null!;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();


            CreateDbContext();
            await CreateAPIRequestContext();
        }

        private void CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<EFContext>().Options;
            IOptions<DatabaseOptions> databaseOptions = Options.Create(new DatabaseOptions { ConnectionString = DATABASE_CONNECTION! });

            Context = new EFContext(options, databaseOptions);
        }

        private async Task CreateAPIRequestContext()
        {
            Request = await this.Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = API_URL,
                IgnoreHTTPSErrors = true,
                ExtraHTTPHeaders = new Dictionary<string, string>()
                {
                    { "Accept", "application/json" }
                }
            });
        }

        public override async Task DisposeAsync()
        {
            await base.DisposeAsync();

            await Context.DisposeAsync();
            await Request.DisposeAsync();
        }
    }
}
