using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinhaLojaExpress.API.Controllers;
using MinhaLojaExpress.Aplicacao.features.Commands.Desconto;
using MinhaLojaExpress.Aplicacao.features.Queries.Desconto;
using MinhaLojaExpress.Aplicacao.Models.Desconto;
using Moq;

namespace MinhaLojaExpress.Testes.Unit.API.Controllers
{
    public class DescontoControllerTeste
    {
        private readonly Mock<IMediator> _mediatorMock;

        public DescontoControllerTeste()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetAll_DeveChamarListarDescontoQuery()
        {
           var controller = new DescontoController(_mediatorMock.Object);
           
           var result = await controller.GetAll();
           
           _mediatorMock.Verify(x => x.Send(It.IsAny<ListarDescontoQuery>(),
               It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_RetornaOkObjectResult()
        {
            var controller = new DescontoController(_mediatorMock.Object);
            
            _mediatorMock.Setup(m => m.Send(It.IsAny<ListarDescontoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<DescontoModel>());

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_DeveChamarObterDescontoQuery()
        {
            var controller = new DescontoController(_mediatorMock.Object);
            var id = Guid.NewGuid();
            
            var result = await controller.Get(id);
            
            _mediatorMock.Verify(x => x.Send(It.IsAny<ObterDescontoQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Fact]
        public async Task Get_QuandoDescontoExistente_RetornaOkObjectResult()
        {
            var controller = new DescontoController(_mediatorMock.Object);
            var id = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<ObterDescontoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DescontoModel());

            var result = await controller.Get(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_QuandoDescontoNaoEncontrado_RetornaNotFoundResult()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<ObterDescontoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DescontoModel)null);
            
            var controller = new DescontoController(_mediatorMock.Object);
            var id = Guid.NewGuid();

            var result = await controller.Get(id);

            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public async Task Post_DeveChamarCriarDescontoCommand()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<CriarDescontoCommand>(), It.IsAny<CancellationToken>()))!
                .ReturnsAsync((DescontoModel)new()
                {
                    Id = Guid.NewGuid()
                });
            
            var controller = new DescontoController(_mediatorMock.Object);
            var desconto = new CriarDescontoCommand("CODIGO", 10, DateTime.Now.AddDays(10), new List<string>());

            var result = await controller.Post(desconto);

            _mediatorMock.Verify(x => x.Send(It.IsAny<CriarDescontoCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Post_RetornaCreatedAtRouteResult()
        {
            var controller = new DescontoController(_mediatorMock.Object);
            var desconto = new CriarDescontoCommand("CODIGO", 10, DateTime.Now.AddDays(10), new List<string>());

            _mediatorMock.Setup(m => m.Send(It.IsAny<CriarDescontoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DescontoModel { Id = Guid.NewGuid() });

            var result = await controller.Post(desconto);

            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
