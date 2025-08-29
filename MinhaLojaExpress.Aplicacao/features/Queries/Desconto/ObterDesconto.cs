using AutoMapper;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Desconto;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Queries.Desconto
{
    public record ObterDescontoQuery(Guid Id) : IRequest<DescontoModel>;
    
    public class ObterDescontoHandler(IDescontoRepositorio descontoRepositorio, IMapper mapper) : IRequestHandler<ObterDescontoQuery, DescontoModel>
    {
        public async Task<DescontoModel> Handle(ObterDescontoQuery request, CancellationToken cancellationToken)
        {
            var desconto = await descontoRepositorio.GetByIdAsync(request.Id);
            return mapper.Map<DescontoModel>(desconto);
        }
    }
}
