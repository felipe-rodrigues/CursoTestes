using Bogus;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Shared.Fakers
{
    public sealed class ItemFakeGenerator : Faker<Item>
    {
        public ItemFakeGenerator() : base("pt_BR")
        {
            RuleFor(i => i.Id, _ => Guid.NewGuid());
            RuleFor(i => i.Nome, f => f.Commerce.ProductName());
            RuleFor(i => i.Quantidade, f => f.Random.Long(10, 10000));
            RuleFor(i => i.Preco, f => decimal.Parse(f.Commerce.Price()));
        }
    }
}
