namespace MinhaLojaExpress.Aplicacao.Models.Pedido
{
    public class ItemPedidoModel
    {
        public string ItemId { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
