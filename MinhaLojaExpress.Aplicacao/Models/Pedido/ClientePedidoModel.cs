namespace MinhaLojaExpress.Aplicacao.Models.Pedido
{
    public class ClientePedidoModel
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
    }
}
