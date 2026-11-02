using RestWithASPNET10Erudio.Configurations;
using Testcontainers.MsSql;

namespace RestWithASPNET10Erudio.Tests.IntegrationTests.Tools
{
    public class SqlServerFixture : IAsyncLifetime
    {
        public MsSqlContainer Container { get; }

        public string ConnectionString => Container.GetConnectionString();

        public SqlServerFixture()
        {
            Container = new MsSqlBuilder()
                .WithPassword("@Admin123$")
                .Build();
        }
        public async ValueTask InitializeAsync()
        {
            await Container.StartAsync();
            EvolveConfig.ExecuteMigrations(ConnectionString);
        }

        public async ValueTask DisposeAsync()
        {
            await Container.DisposeAsync();
        }

    }
}
