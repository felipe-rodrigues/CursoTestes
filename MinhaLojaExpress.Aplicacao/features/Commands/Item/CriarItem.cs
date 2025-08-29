using AutoMapper;
using FluentValidation;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Item;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Commands.Item
{
    public record CriarItemCommand(string Nome, int Quantidade, decimal Preco) : IRequest<ItemModel>;

    public class CriarItemCommandValidator : AbstractValidator<CriarItemCommand>
    {
        public CriarItemCommandValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Quantidade).GreaterThan(0);
            RuleFor(x => x.Preco).GreaterThan(0);
        }
    }
    
    public class CriarItemCommandHandler(IItemRepositorio itemRepositorio, IMapper mapper)
        : IRequestHandler<CriarItemCommand, ItemModel>
    {
        public async Task<ItemModel> Handle(CriarItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Dominio.Entidades.Item()
            {
                Nome = request.Nome,
                Preco = request.Preco,
                Quantidade = request.Quantidade
            };
            
            await itemRepositorio.AddAsync(item);
            return mapper.Map<ItemModel>(item);
        }
    }
}
