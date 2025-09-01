using AutoMapper;
using FluentValidation;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Pedido;
using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Commands.Pedido
{
    public record CriarPedidoCommand(string ClienteId, Dictionary<string, int> Items, IEnumerable<string>? DescontoCodigos) : IRequest<PedidoModel>;
    
    public class CriarPedidoCommandValidator : AbstractValidator<CriarPedidoCommand>
    {
        public CriarPedidoCommandValidator(IDescontoRepositorio descontoRepositorio)
        {
            RuleFor(x => x.ClienteId).NotEmpty().WithMessage("O ID do cliente é obrigatório.");

            RuleForEach(x => x.Items)
                .Must(i => !string.IsNullOrWhiteSpace(i.Key))
                .WithMessage("O ID do item é obrigatório.");

            RuleForEach(x => x.Items)
                .Must(i => i.Value > 0)
                .WithMessage("A quantidade do item deve ser maior que zero.");

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

                    var descontoValidos = descontosValidos.Select(d => d.Codigo.ToString()).ToHashSet();

                    foreach (var id in codigos)
                    {
                        if (!descontoValidos.Contains(id))
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
                request.Items.Keys.ToList().Contains(i.Id.ToString()));
            
            var descontos = request.DescontoCodigos != null
                ? await descontoRepositorio.GetAsync(d =>
                    request.DescontoCodigos.Contains(d.Codigo) && d.DataValidade > DateTime.Now)
                : [];

            var pedidoItems = items.Select(item =>
            {
                var quantidade = request.Items[item.Id.ToString()];
                var desconto = descontos
                    .Where(d => d.Items == null || d.Items.Any(i => i.Id == item.Id))
                    .OrderByDescending(d => d.Percentual)
                    .FirstOrDefault();

                var valorUnitario = desconto != null
                    ? item.Preco * (1 - desconto.Percentual / 100m)
                    : item.Preco;

                return new ItemPedido
                {
                    ItemId = item.Id,
                    Quantidade = quantidade,
                    Valor = valorUnitario
                };
            }).ToList();

            var pedido = new Dominio.Entidades.Pedido
            {
                ClienteId = Guid.Parse(request.ClienteId),
                Items = pedidoItems,
                DataPedido = DateTime.UtcNow,
                ValorTotal = pedidoItems.Sum(i => i.Valor * i.Quantidade)
            };

            await pedidoRepositorio.AddAsync(pedido);
            
            return mapper.Map<PedidoModel>(pedido);
        }
    }

}
