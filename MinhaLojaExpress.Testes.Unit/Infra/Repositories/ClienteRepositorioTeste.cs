using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Infra.Repositorios;
using Xunit.Abstractions;

namespace MinhaLojaExpress.Testes.Unit.Infra.Repositories
{
    public class ClienteRepositorioTeste(ITestOutputHelper output, Database db) : IClassFixture<Database>, IAsyncLifetime
    {
        [Fact]
        public async Task GetById_QuandoItemExistente_Retorna()
        {
            output.WriteLine($"Usando banco de dados: {db.DataBaseName}");
            var repositorio = new ClienteRepositorio(db.Context);
            var idCliente = Guid.Parse("d7a8ac65-9321-45f2-9afb-b5ebb8b414ca");
            var cliente = new Cliente
            {
                Id = idCliente, Nome = "Teste", Email = "teste@teste.com", Telefone = "11999999999"
            };
            await repositorio.AddAsync(cliente);
            var clienteRetornado = await repositorio.GetByIdAsync(cliente.Id);
            Assert.Equal(cliente.Nome, clienteRetornado?.Nome);
        }
        
        public async Task DisposeAsync()
        {
            var clientes = db.Context.Clientes.ToList();
            db.Context.Clientes.RemoveRange(clientes);
        }
    }
}
