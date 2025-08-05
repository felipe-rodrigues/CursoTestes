using AutoMapper;

namespace MinhaLojaExpress.Aplicacao.Mapper.Item
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<MinhaLojaExpress.Dominio.Entidades.Item, Models.Item.ItemModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco));
        }
    }
}
