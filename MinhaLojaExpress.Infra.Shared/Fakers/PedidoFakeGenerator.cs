using Bogus;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Shared.Fakers
{
    public sealed class PedidoFakeGenerator : Faker<Pedido>
    {
        public PedidoFakeGenerator() : base("pt_BR")
        {
            RuleFor(p => p.Id, _ => Guid.NewGuid());
            RuleFor(p => p.ValorTotal, (f, src) => src.Items.Sum(i => i.Valor)); // Valor total entre 50 e 500
            RuleFor(p => p.DataPedido, f => f.Date.Recent(30).ToUniversalTime());
        }

        public List<Pedido> GerarPedidosParaClientes(List<Cliente> clientes, List<Item> todosOsItems)
        {
            var pedidos = new List<Pedido>();
            foreach (var cliente in clientes)
            {
                var newPedido = Generate(cliente, todosOsItems);
                pedidos.Add(newPedido);
            } 
            
            return pedidos;
        }

        public Pedido Generate(Cliente cliente, List<Item> items)
        {
            var pedido = Generate();
            pedido.ClienteId = cliente.Id;
            pedido.Cliente = cliente;

            var itemstoAdd = new List<Item>()
                { FakerHub.PickRandom(items), FakerHub.PickRandom(items), FakerHub.PickRandom(items) };

            pedido.Items = itemstoAdd.Select(i => new ItemPedido()
            {
                Quantidade = FakerHub.Random.Int(1, 10),
                ItemId = i.Id,
                PedidoId = pedido.Id,
                Valor = FakerHub.Finance.Amount(10, 100),
            }).ToList();

            return pedido;
        }
    }
}
