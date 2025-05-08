namespace MinhaLojaExpress.Dominio.Entidades
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
