using System.Net.Http.Json;
using MinhaLojaExpress.Aplicacao.features.Commands.Pedido;
using MinhaLojaExpress.Aplicacao.Models.Pedido;
using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Infra.Shared.Fakers;

namespace MinhaLojaExpress.Testes.Integracao.API.Controllers
{
    [Collection("Teste API")]
    public class PedidoController : IAsyncLifetime
    {
        private readonly ApiFactory _api;

        public PedidoController(ApiFactory api)
        {
            _api = api;
        }

        [Fact]
        public async Task Post_QuandoItemsExistentesSemDesconto_AdicionaPedido()
        {
            var item = new ItemFakeGenerator().Generate();
            await _api.AdicionarRegistro(item);

            var cliente = new ClienteFakeGenerator().Generate();
            await _api.AdicionarRegistro(cliente);

            var criarPedidoCommand = new CriarPedidoCommand(
                cliente.Id.ToString(), new Dictionary<string, int>()
                {
                    { item.Id.ToString(), 1 }
                }, null);
            
            var responsePedido = await _api.Client.PostAsJsonAsync("api/Pedido", criarPedidoCommand);
            responsePedido.EnsureSuccessStatusCode();
            var response = await responsePedido.Content.ReadFromJsonAsync<PedidoModel>();
            Assert.NotNull(response);
            Assert.Equal(1, response.Items.Count());
            Assert.Equal(item.Preco, response.Items.First().Valor);
        }

        public async Task InitializeAsync()
        {
           
        }

        public async Task DisposeAsync()
        {
            await _api.RemoverRegistros<Pedido>();
            await _api.RemoverRegistros<Item>();
            await _api.RemoverRegistros<Cliente>();
        }
    }
}
