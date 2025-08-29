using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinhaLojaExpress.Aplicacao.features.Commands.Pedido;
using MinhaLojaExpress.Aplicacao.features.Queries.Pedido;

namespace MinhaLojaExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var pedido = await mediator.Send(new ObterPedidoQuery(id));

            if (pedido is null)
                return NotFound();

            return Ok(pedido);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarPedidoCommand command)
        {
            var pedido = await mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }
    }
}
