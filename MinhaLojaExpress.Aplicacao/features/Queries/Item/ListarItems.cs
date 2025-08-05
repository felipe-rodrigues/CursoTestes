using AutoMapper;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Item;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Queries.Item
{
    public record ListarItemsQuery : IRequest<IEnumerable<ItemModel>>;

    public class ListarItemsQueryHandler : IRequestHandler<ListarItemsQuery, IEnumerable<ItemModel>>
    {
        private readonly IItemRepositorio _itemRepositorio;
        private readonly IMapper _mapper;

        public ListarItemsQueryHandler(IItemRepositorio itemRepository, IMapper mapper)
        {
            _itemRepositorio = itemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemModel>> Handle(ListarItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _itemRepositorio.GetAllAsync();
            var mappedItems = _mapper.Map<IEnumerable<ItemModel>>(items);
            return mappedItems;
        }
    }
}
