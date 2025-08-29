namespace MinhaLojaExpress.Aplicacao.Models.Pedido
{
    public class DescontoPedidoModel
    {
        public string Id { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public decimal Percentual { get; set; }
    }
}
