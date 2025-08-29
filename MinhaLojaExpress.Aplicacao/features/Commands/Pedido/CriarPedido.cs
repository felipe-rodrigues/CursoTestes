using AutoMapper;
using FluentValidation;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Pedido;
using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Commands.Pedido
{
    public record CriarPedidoCommand(string ClienteId, IEnumerable<string> ItemIds, IEnumerable<string>? DescontoCodigos) : IRequest<PedidoModel>;
    
    public class CriarPedidoCommandValidator : AbstractValidator<CriarPedidoCommand>
    {
        public CriarPedidoCommandValidator(IDescontoRepositorio descontoRepositorio)
        {
            RuleFor(x => x.ClienteId).NotEmpty().WithMessage("O ID do cliente é obrigatório.");
            RuleFor(x => x.ItemIds).NotEmpty().WithMessage("É necessário pelo menos um item para criar um pedido.");

            RuleFor(x => x.DescontoCodigos)
                .CustomAsync(async (codigos, context, cancellation) =>
                {
                    if (codigos == null || !codigos.Any())
                        return;

                    var descontosValidos = await descontoRepositorio.GetAsync(d =>
                    {
                        var enumerable = codigos as string[] ?? codigos.ToArray();
                        return enumerable.Contains(d.Codigo) && d.DataValidade > DateTime.Now;
                    });

                    var descontosValidosIds = descontosValidos.Select(d => d.Id.ToString()).ToHashSet();

                    foreach (var id in codigos)
                    {
                        if (!descontosValidosIds.Contains(id))
                        {
                            context.AddFailure($"Desconto {id} inválido ou expirado.");
                        }
                    }
                });
        }
    }
    
    public class CriarPedidoCommandHandler(
        IItemRepositorio itemRepositorio,
        IDescontoRepositorio descontoRepositorio,
        IPedidoRepositorio pedidoRepositorio,
        IMapper mapper) : IRequestHandler<CriarPedidoCommand, PedidoModel>
    {
        public async Task<PedidoModel> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            var items = await itemRepositorio.GetAsync(i =>
                request.ItemIds.ToList().Contains(i.Id.ToString()));
            
            var pedido = new Dominio.Entidades.Pedido
            {
                ClienteId = Guid.Parse(request.ClienteId),
                Items = items.Select(item => new ItemPedido
                    {
                        ItemId = item.Id,
                        Quantidade = 1, // Adjust as needed
                        Valor = item.Preco
                    })
                    .ToList(),
                DataPedido = DateTime.UtcNow,
                ValorTotal = items.Sum(item => item.Preco)
            };

            await pedidoRepositorio.AddAsync(pedido);
            
            return mapper.Map<PedidoModel>(pedido);
        }
    }

}
