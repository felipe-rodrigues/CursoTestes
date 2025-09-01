using AutoMapper;
using MinhaLojaExpress.Aplicacao.Mapper.Cliente;
using MinhaLojaExpress.Aplicacao.Mapper.Desconto;
using MinhaLojaExpress.Aplicacao.Mapper.Item;
using MinhaLojaExpress.Aplicacao.Mapper.Pedido;

namespace MinhaLojaExpress.Testes.Unit.Aplicacao.features
{
    public class HandlerTesteBase
    {
        public IMapper Mapper { get; set; }
        
        public HandlerTesteBase()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClienteProfile());
                cfg.AddProfile(new DescontoProfile());
                cfg.AddProfile(new PedidoProfile());
                cfg.AddProfile(new ItemProfile());
            }).CreateMapper();
        }
    }
}
