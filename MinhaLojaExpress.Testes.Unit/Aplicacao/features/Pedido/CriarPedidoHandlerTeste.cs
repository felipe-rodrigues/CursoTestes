using MinhaLojaExpress.Aplicacao.features.Commands.Pedido;
using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using Moq;

namespace MinhaLojaExpress.Testes.Unit.Aplicacao.features.Pedido
{
    public class CriarPedidoHandlerTeste : HandlerTesteBase
    {
        private readonly Mock<IItemRepositorio> _itemRepositorioMock;
        private readonly Mock<IDescontoRepositorio> _descontoRepositorioMock;
        private readonly Mock<IPedidoRepositorio> _pedidoRepositorioMock;
        private readonly CriarPedidoCommandHandler _handler;

        public CriarPedidoHandlerTeste()
        {
            _itemRepositorioMock = new Mock<IItemRepositorio>();
            _descontoRepositorioMock = new Mock<IDescontoRepositorio>();
            _pedidoRepositorioMock = new Mock<IPedidoRepositorio>();
        }

        [Fact]
        public async Task Handle_QuandoItemsExistentes_RetornaComCalculoExato()
        {
            var item1Id = Guid.NewGuid();
            var item2Id = Guid.NewGuid();
            var items = new List<Item>()
            {
                new Item()
                {
                    Id = item1Id,
                    Nome = "Item 1",
                    Preco = 5m
                },
                new Item()
                {
                    Id = item2Id,
                    Nome = "Item 2",
                    Preco = 10m
                }
            };
            
            _itemRepositorioMock.Setup(s => s.GetAsync(It.IsAny<Func<Item, bool>>()))
                .ReturnsAsync(items);
            
            var handler = new CriarPedidoCommandHandler(
                _itemRepositorioMock.Object,
                _descontoRepositorioMock.Object,
                _pedidoRepositorioMock.Object,
                Mapper);
            
            var resultado = await handler.Handle(new CriarPedidoCommand(
                ClienteId: Guid.NewGuid().ToString(),
                Items: new Dictionary<string, int>
                {
                    { item1Id.ToString(), 1 },
                    { item2Id.ToString(), 1 }
                },
                DescontoCodigos: null), CancellationToken.None);
            
            Assert.Equal(15m,resultado.ValorTotal);
        }


        [Fact]
        public async Task Handle_QuandoItemsExistentesComDesconto_RetornaCalculoExato()
        {
            var item1Id = Guid.NewGuid();
            var item2Id = Guid.NewGuid();
            var cupom = "100OFF";
            var item1 = new Item()
            {
                Id = item1Id,
                Nome = "Item 1",
                Preco = 5m
            };
            var item2 = new Item()
            {
                Id = item2Id,
                Nome = "Item 2",
                Preco = 10m
            };
            
            var items = new List<Item>()
            {
                item1,
                item2
            };

            _itemRepositorioMock.Setup(s => s.GetAsync(It.IsAny<Func<Item, bool>>()))
                .ReturnsAsync(items);
            
            _descontoRepositorioMock.Setup(d => d.GetAsync(It.IsAny<Func<Desconto,bool>>()))
                .ReturnsAsync(new List<Desconto>()
                {
                    new Desconto() { Codigo = cupom, Percentual = 100m, DataValidade = DateTime.Now.AddDays(10), Items = new List<Item>()
                    {
                        item1
                    }}
                });
            
            var handler = new CriarPedidoCommandHandler(
                _itemRepositorioMock.Object,
                _descontoRepositorioMock.Object,
                _pedidoRepositorioMock.Object,
                Mapper);
            
            var resultado = await handler.Handle(new CriarPedidoCommand(
                ClienteId: Guid.NewGuid().ToString(),
                Items: new Dictionary<string, int>
                {
                    { item1Id.ToString(), 1 },
                    { item2Id.ToString(), 1 }
                },
                DescontoCodigos: new List<string> { cupom }), CancellationToken.None);
         

            Assert.Equal(10m, resultado.ValorTotal);
        }
    }
}
