using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Infra.Repositorios;
using Xunit.Abstractions;

namespace MinhaLojaExpress.Testes.Unit.Infra.Repositories
{
    public class PedidoRepositorioTeste(ITestOutputHelper output, Database db) : IClassFixture<Database>
    {
        [Fact]
        public async Task AddAsync_ParaCliente_SalvaNoBanco()
        {
            output.WriteLine($"Usando banco de dados: {db.DataBaseName}");
            var repositorioCliente = new ClienteRepositorio(db.Context);
            var idCliente = Guid.Parse("d7a8ac65-9321-45f2-9afb-b5ebb8b414ca");
            var cliente = new Cliente
            {
                Id = idCliente,
                Nome = "Teste",
                Email = "teste@teste.com",
                Telefone = "11999999999"
            };
            await repositorioCliente.AddAsync(cliente);
            
            var repositorioPedido = new PedidoRepositorio(db.Context);
            var pedido = new Pedido
            {
                Id = Guid.NewGuid(),
                ClienteId = idCliente,
                Items = new List<ItemPedido>(),
                DataPedido = DateTime.Now,
                ValorTotal = 100
            };
            await repositorioPedido.AddAsync(pedido);
        }
    }
}
