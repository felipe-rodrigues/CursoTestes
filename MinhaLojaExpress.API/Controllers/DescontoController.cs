using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinhaLojaExpress.Aplicacao.features.Commands.Desconto;
using MinhaLojaExpress.Aplicacao.features.Queries.Desconto;
using MinhaLojaExpress.Aplicacao.features.Queries.Item;
using MinhaLojaExpress.Aplicacao.Models.Desconto;

namespace MinhaLojaExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescontoController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new ListarDescontoQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await mediator.Send(new ObterDescontoQuery(id));

            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CriarDescontoCommand desconto)
        {
            var result = await mediator.Send(desconto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}
