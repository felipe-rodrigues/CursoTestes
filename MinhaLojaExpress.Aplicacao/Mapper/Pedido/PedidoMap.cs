using AutoMapper;
using MinhaLojaExpress.Aplicacao.Models.Pedido;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Aplicacao.Mapper.Pedido
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<ItemPedido, ItemPedidoModel>()
                .ForMember(i => i.Nome, opt => opt.MapFrom(src => src.Item.Nome));
            CreateMap<Dominio.Entidades.Pedido, PedidoModel>();
        }
    }
}
