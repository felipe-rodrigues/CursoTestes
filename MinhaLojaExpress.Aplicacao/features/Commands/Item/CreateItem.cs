using AutoMapper;
using FluentValidation;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Item;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Commands.Item
{
    public record CreateItemCommand(string Nome, int Quantidade, decimal Preco) : IRequest<ItemModel>;

    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Quantidade).GreaterThan(0);
            RuleFor(x => x.Preco).GreaterThan(0);
        }
    }
    
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemModel>
    {
        private readonly IItemRepositorio _itemRepositorio;
        private readonly IMapper _mapper;
        
        public CreateItemCommandHandler(IItemRepositorio itemRepositorio, IMapper mapper)
        {
            _itemRepositorio = itemRepositorio;
            _mapper = mapper;
        }
        
        public async Task<ItemModel> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Dominio.Entidades.Item()
            {
                Nome = request.Nome,
                Preco = request.Preco,
                Quantidade = request.Quantidade
            };
            
            await _itemRepositorio.AddAsync(item);
            return _mapper.Map<ItemModel>(item);
        }
    }
}
