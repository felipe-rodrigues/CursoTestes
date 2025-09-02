using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MinhaLojaExpress.API.Infra;
using MinhaLojaExpress.Infra.Contexto;
using Testcontainers.PostgreSql;

namespace MinhaLojaExpress.Testes.Integracao.API.Controllers
{
    public class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _database = new PostgreSqlBuilder()
            .WithDatabase("minhalojaexpress")
            .Build();
        
        public HttpClient Client { get; private set; }
        public string ApiId { get; init; }
        
        private MinhaLojaExpressContext _dbContext { get; set; }

        public ApiFactory()
        {
            ApiId = Guid.NewGuid().ToString();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(MinhaLojaExpressContext));
                services.RemoveAll(typeof(DbContextOptions));
                
                var connectionString = _database.GetConnectionString();
                services.AddDbContext<MinhaLojaExpressContext>(o => o.UseNpgsql(connectionString));

                var db = services.BuildServiceProvider().GetRequiredService<MinhaLojaExpressContext>();
                _dbContext = db;
                db.Database.EnsureCreated();
            });
        }

        public async Task AdicionarRegistro<T>(T item) where T : class
        {
            await _dbContext.Set<T>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoverRegistros<T>() where T : class
        {
            var registros = await _dbContext.Set<T>().ToListAsync();
            _dbContext.Set<T>().RemoveRange(registros);
            await _dbContext.SaveChangesAsync();
        }


        public async Task InitializeAsync()
        {
            await _database.StartAsync();
            Client = CreateClient();
        }

        public async Task DisposeAsync()
        {
            await _database.StopAsync();
            Client.Dispose();
        }
    }
}
