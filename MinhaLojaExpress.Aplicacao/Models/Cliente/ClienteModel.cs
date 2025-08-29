using MinhaLojaExpress.Aplicacao.Models.Pedido;

namespace MinhaLojaExpress.Aplicacao.Models.Cliente
{
    public class ClienteModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public ICollection<PedidoModel>? Pedidos { get; set; }
    }
}
