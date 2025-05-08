namespace MinhaLojaExpress.Dominio.Entidades
{
    public class ItemPedido
    {
        public Guid ItemId { get; set; }
        public Guid PedidoId { get; set; }
        public long Quantidade { get; set; }
        public Item Item { get; set; } = null!;
        public Pedido Pedido { get; set; } = null!;
    }
}
