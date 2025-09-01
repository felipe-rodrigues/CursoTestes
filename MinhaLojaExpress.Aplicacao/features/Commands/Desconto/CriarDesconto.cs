using AutoMapper;
using FluentValidation;
using MediatR;
using MinhaLojaExpress.Aplicacao.Models.Desconto;
using MinhaLojaExpress.Dominio.Interfaces;

namespace MinhaLojaExpress.Aplicacao.features.Commands.Desconto
{
    public record CriarDescontoCommand(string Codigo, decimal Percentual, DateTime DataValidade, IEnumerable<string>? ItemIds) : IRequest<DescontoModel>;

    public abstract class CriarDescontoCommandValidator : AbstractValidator<CriarDescontoCommand>
    {
        protected CriarDescontoCommandValidator(IItemRepositorio itemRepositorio)
        {
            RuleFor(c => c.Codigo)
                .NotEmpty()
                .WithMessage("O código do desconto é obrigatório.")
                .MaximumLength(10)
                .WithMessage("O código do desconto deve ter no máximo 10 caracteres.");

            RuleFor(c => c.Percentual)
                .NotEmpty()
                .WithMessage("O Percentual do desconto é obrigatório.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O Percentual do desconto deve ser maior ou igual a 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("O Percentual do desconto deve ser menor ou igual a 100.");

            RuleFor(c => c.DataValidade)
                .NotEmpty()
                .WithMessage("A data de validade do desconto é obrigatória.")
                .GreaterThan(DateTime.Now)
                .WithMessage("A data de validade do desconto deve ser maior que a data atual.");

            RuleFor(c => c.ItemIds)
                .Must(ids => ids == null || ids.Any())
                .When(c => c.ItemIds != null)
                .WithMessage("A lista de IDs de itens não pode estar vazia.")
                .MustAsync(async (command, ids, cancellation) =>
                {
                    if (ids == null) return true;
                    var existentes = await itemRepositorio.ListarExistentes(ids.ToList());
                    return existentes.Count() == ids.Count();
                })
                .WithMessage("Um ou mais IDs de itens são inválidos.");
        }
        
        public class CriarDescontoCommandHandler(IDescontoRepositorio descontoRepositorio, IItemRepositorio itemRepositorio, IMapper mapper)
            : IRequestHandler<CriarDescontoCommand, DescontoModel>
        {
            public async Task<DescontoModel> Handle(CriarDescontoCommand request, CancellationToken cancellationToken)
            {
                var items = await itemRepositorio.GetAsync(i =>
                    request.ItemIds != null && request.ItemIds.ToList().Contains(i.Id.ToString()));
                var desconto = new Dominio.Entidades.Desconto()
                {
                    Codigo = request.Codigo,
                    Percentual = request.Percentual,
                    DataValidade = request.DataValidade.ToUniversalTime(),
                    Items = items
                };

                await descontoRepositorio.AddAsync(desconto);

                return mapper.Map<DescontoModel>(desconto);
            }
        }
    }

}
