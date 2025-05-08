namespace MinhaLojaExpress.Dominio.Entidades
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public long Quantidade { get; set; }
        public decimal Preco { get; set; }
        public IEnumerable<Desconto>? Descontos { get; set; }
    }
}
