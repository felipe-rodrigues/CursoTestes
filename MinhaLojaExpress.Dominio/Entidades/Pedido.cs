namespace MinhaLojaExpress.Dominio.Entidades
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public required IEnumerable<ItemPedido> Items { get; set; }
        public IEnumerable<Desconto>? Descontos { get; set; }
        public decimal ValorTotal { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public DateTime DataPedido { get; set; }
    }
}