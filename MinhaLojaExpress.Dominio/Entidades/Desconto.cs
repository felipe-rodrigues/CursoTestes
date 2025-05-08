namespace MinhaLojaExpress.Dominio.Entidades
{
    public class Desconto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public decimal Percentual { get; set; }
        public DateTime DataValidade { get; set; }
        public IEnumerable<Item>? Items { get; set; }
        public IEnumerable<Pedido>? Pedidos { get; set; }
    }
}
