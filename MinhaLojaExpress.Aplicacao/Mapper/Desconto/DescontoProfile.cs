using AutoMapper;
using MinhaLojaExpress.Aplicacao.Models.Desconto;

namespace MinhaLojaExpress.Aplicacao.Mapper.Desconto
{
    public class DescontoProfile : Profile
    {
        public DescontoProfile()
        {
            CreateMap<Dominio.Entidades.Desconto, DescontoModel>();
        }
    }
}
