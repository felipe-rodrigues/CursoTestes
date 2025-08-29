using AutoMapper;
using MinhaLojaExpress.Aplicacao.Models.Pedido;

namespace MinhaLojaExpress.Aplicacao.Mapper.Cliente
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Dominio.Entidades.Cliente, ClientePedidoModel>();
        }
    }
}
