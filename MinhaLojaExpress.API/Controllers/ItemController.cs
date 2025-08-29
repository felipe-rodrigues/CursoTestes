using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinhaLojaExpress.Aplicacao.features.Commands.Item;
using MinhaLojaExpress.Aplicacao.features.Queries.Item;

namespace MinhaLojaExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new ListarItemsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
           var item = await mediator.Send(new ObterItemQuery(id));
           
           if (item is null)
               return NotFound();
           
           return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarItemCommand itemCommandModel)
        {
            var result = await mediator.Send(itemCommandModel);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}
