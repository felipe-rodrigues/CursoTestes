using Bogus;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Shared.Fakers
{
    public sealed class ClienteFakeGenerator : Faker<Cliente>
    {
        public ClienteFakeGenerator() : base("pt_BR")
        {
            RuleFor(c => c.Id, _ => Guid.NewGuid());
            RuleFor(c => c.Nome, f => f.Person.FullName);
            RuleFor(c => c.Email, f => f.Internet.Email());
            RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("(##) #####-####"));
        }
    }
}
