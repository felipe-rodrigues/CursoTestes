using MinhaLojaExpress.Aplicacao.Models.Item;

namespace MinhaLojaExpress.Aplicacao.Models.Desconto
{
    public class DescontoModel
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public decimal Percentual { get; set; }
        public DateTime DataValidade { get; set; }
        public IEnumerable<ItemModel> Items { get; set; }
    }
}
