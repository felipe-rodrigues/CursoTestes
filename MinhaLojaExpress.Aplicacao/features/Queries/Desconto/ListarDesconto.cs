using AutoMapper;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Desconto;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Queries.Desconto
{
    public record ListarDescontoQuery : IRequest<IEnumerable<DescontoModel>>;
    
    public class ListarDescontoHandler(IDescontoRepositorio descontoRepositorio, IMapper mapper) : IRequestHandler<ListarDescontoQuery, IEnumerable<DescontoModel>>
    {
        public async Task<IEnumerable<DescontoModel>> Handle(ListarDescontoQuery request, CancellationToken cancellationToken)
        {
            var desconto = await descontoRepositorio.GetAllAsync();
            return mapper.Map<IEnumerable<DescontoModel>>(desconto);
        }
    }
}
