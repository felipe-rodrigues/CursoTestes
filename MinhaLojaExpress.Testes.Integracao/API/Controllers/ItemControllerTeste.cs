using MinhaLojaExpress.Aplicacao.Models.Item;
using MinhaLojaExpress.Dominio.Entidades;
using System.Net.Http.Json;

namespace MinhaLojaExpress.Testes.Integracao.API.Controllers
{
    [Collection("Teste API")]
    public class ItemControllerTest : IAsyncLifetime
    {
        private readonly ApiFactory _api;
        
        public ItemControllerTest(ApiFactory api)
        {
            _api = api;
        }

        [Fact]
        public async Task GetAll_QuandoDados_RetornaLista()
        {
            var item = new Item()
            {
                Nome = "Nome",
                Preco = 10m,
                Quantidade = 100,
                Id = Guid.NewGuid()
            };
            await _api.AdicionarRegistro(item);
            
            var response = await _api.Client.GetAsync("api/Item");
            response.EnsureSuccessStatusCode();
            var items = await response.Content.ReadFromJsonAsync<List<ItemModel>>();
            Assert.NotEmpty(items);
        }

        public async Task InitializeAsync()
        {
            
        }

        public async Task DisposeAsync()
        {
            await _api.RemoverRegistros<Item>();
        }
    }
}
