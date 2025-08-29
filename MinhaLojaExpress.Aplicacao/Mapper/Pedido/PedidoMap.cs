using AutoMapper;
using MinhaLojaExpress.Aplicacao.Models.Pedido;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Aplicacao.Mapper.Pedido
{
    public class PedidoMap : Profile
    {
        public PedidoMap()
        {
            CreateMap<ItemPedido, ItemPedidoModel>()
                .ForMember(i => i.Nome, opt => opt.MapFrom(src => src.Item.Nome));
            CreateMap<Dominio.Entidades.Pedido, PedidoModel>();
        }
    }
}
