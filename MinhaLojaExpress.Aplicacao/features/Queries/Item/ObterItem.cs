using AutoMapper;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Item;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Queries.Item
{
    public record ObterItemQuery(Guid Id) : IRequest<ItemModel?>;

    public class ObterItemQueryHandler : IRequestHandler<ObterItemQuery, ItemModel?>
    {
        private readonly IItemRepositorio _itemRepositorio;
        private readonly IMapper _mapper;

        public ObterItemQueryHandler(IItemRepositorio itemRepository, IMapper mapper)
        {
            _itemRepositorio = itemRepository;
            _mapper = mapper;
        }

        public async Task<ItemModel?> Handle(ObterItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _itemRepositorio.GetByIdAsync(request.Id);
            return _mapper.Map<ItemModel?>(item);
        }
    }
}
