using Bogus;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Shared.Fakers
{
    public sealed class DescontoFakeGenerator : Faker<Desconto>
    {
        public DescontoFakeGenerator() : base("pt_BR")
        {
            RuleFor(d => d.Id, _ => Guid.NewGuid());
            RuleFor(d => d.Codigo, f => f.Commerce.ProductAdjective());
            RuleFor(d => d.Percentual, f => decimal.Parse(f.Commerce.Price(1, 100)));
            RuleFor(d => d.DataValidade, f => f.Date.Future().ToUniversalTime());
        }
    }
}
