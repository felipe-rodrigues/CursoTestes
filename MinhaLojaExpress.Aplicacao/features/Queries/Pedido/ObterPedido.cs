using AutoMapper;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Pedido;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Queries.Pedido
{
    public record ObterPedidoQuery(Guid Id) : IRequest<PedidoModel>;
    
    public class ObterPedidoQueryHandler(IPedidoRepositorio pedidoRepositorio, IMapper mapper)
        : IRequestHandler<ObterPedidoQuery, PedidoModel>
    {
        public async Task<PedidoModel> Handle(ObterPedidoQuery request, CancellationToken cancellationToken)
        {
            var pedido = await pedidoRepositorio.GetByIdAsync(request.Id);
            return mapper.Map<PedidoModel>(pedido);
        }
    }
}
