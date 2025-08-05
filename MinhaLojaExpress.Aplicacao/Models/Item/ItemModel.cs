namespace MinhaLojaExpress.Aplicacao.Models.Item
{
    public class ItemModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public long Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
