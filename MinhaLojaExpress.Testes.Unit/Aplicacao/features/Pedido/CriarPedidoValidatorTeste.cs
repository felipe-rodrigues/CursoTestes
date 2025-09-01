using MinhaLojaExpress.Aplicacao.features.Commands.Pedido;
using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using Moq;

namespace MinhaLojaExpress.Testes.Unit.Aplicacao.features.Pedido
{
    public class CriarPedidoValidatorTeste
    {
        private readonly Mock<IDescontoRepositorio> _descontoRepositorioMock;
        
        public CriarPedidoValidatorTeste()
        {
            _descontoRepositorioMock = new Mock<IDescontoRepositorio>();
        }
        
        [Fact]
        public async Task Validate_QuandoClieteNulo_RetornaErro()
        {
            var validator = new CriarPedidoCommandValidator(_descontoRepositorioMock.Object);
            
            var result = await validator.ValidateAsync(new CriarPedidoCommand("", new Dictionary<string, int>(), null));

            Assert.False(result.IsValid);
            Assert.Equal("O ID do cliente é obrigatório.", result.ToString());
        }

        [Fact]
        public async Task Validate_QuandoItemComIdVazioEQuantidadeZero_RetornaInvalido()
        { 
            var validator = new CriarPedidoCommandValidator(_descontoRepositorioMock.Object);
            
            var items = new Dictionary<string, int>
            {
                { "", 0 }
            };
            var result = await validator.ValidateAsync(new CriarPedidoCommand("cliente-123", items, null));

            Assert.False(result.IsValid);
            Assert.Contains("O ID do item é obrigatório.", result.Errors[0].ErrorMessage);
            Assert.Contains("A quantidade do item deve ser maior que zero.", result.Errors[1].ErrorMessage);
        }
        
        [Fact]
        public async Task Validate_QuandoDescontoComCodigoInvalido_RetornaInvalido()
        {
            _descontoRepositorioMock.Setup(d => d.GetAsync(It.IsAny<Func<Desconto, bool>>())).ReturnsAsync(Enumerable.Empty<Desconto>);
            var validator = new CriarPedidoCommandValidator(_descontoRepositorioMock.Object);
            
            var result = await validator.ValidateAsync(new CriarPedidoCommand("cliente-123", new Dictionary<string, int> { { "item-123", 1 } }, new List<string> { "DESC10" }));
            
            Assert.False(result.IsValid);
            Assert.Contains("Desconto DESC10 inválido ou expirado.", result.Errors[0].ErrorMessage);
        }
    }
}
