using AutoMapper;
using Regulatorio.Domain.Entities.Dominios;
using Regulatorio.Domain.Response.Dominios;

namespace Regulatorio.Core.Mappers.Dominios
{
    public class DominioDtoProfile : Profile
    {
        public DominioDtoProfile()
        {
            CreateMap<TipoDominio, TipoDominioResponse>()
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
