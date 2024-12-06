using AutoMapper;
using Regulatorio.Domain.Entities.Dominios;
using Regulatorio.Domain.Response.Dominios;

namespace Regulatorio.Core.Mappers.Dominios
{
    public class TipoDominioProfile : Profile
    {
        public TipoDominioProfile()
        {
            CreateMap<Dominio, DominioResponse>()
               .ForMember(dest => dest.TipoDominio, opt => opt.MapFrom(src => src.TipoDominio))
               .ForMember(dest => dest.ValorDominio, opt => opt.MapFrom(src => src.Valor));
        }
    }
}
