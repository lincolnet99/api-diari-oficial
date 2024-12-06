using AutoMapper;
using Regulatorio.Domain.DTOs.Garantias;
using Regulatorio.Domain.Response.Garantias;

namespace Regulatorio.Core.Mappers.Garantias
{
    public class GarantiaDtoProfile : Profile
    {
        public GarantiaDtoProfile()
        {
            CreateMap<GarantiaDto, GarantiaResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
               .ForMember(dest => dest.TipoRegistro, opt => opt.MapFrom(src => src.TipoRegistro))
               .ForMember(dest => dest.CriadoEm, opt => opt.MapFrom(src => src.CriadoEm))
               .ForMember(dest => dest.ValorPublico, opt => opt.MapFrom(src => src.ValorPublico))
               .ForMember(dest => dest.ValorPrivado, opt => opt.MapFrom(src => src.ValorPrivado))
               .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao))
               .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal));
        }
    }
}
