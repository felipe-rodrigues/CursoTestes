using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Aplicacao.Models.Pedido
{
    public class PedidoModel
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public required IEnumerable<ItemPedidoModel> Items { get; set; }
        public IEnumerable<DescontoPedidoModel>? Descontos { get; set; }
        public decimal ValorTotal { get; set; }
        public ClientePedidoModel Cliente { get; set; } = null!;
        public DateTime DataPedido { get; set; }
    }
}
