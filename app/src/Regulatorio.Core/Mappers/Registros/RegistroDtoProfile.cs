using AutoMapper;
using Regulatorio.Domain.DTOs.Registros;
using Regulatorio.Domain.Response.Registros;

namespace Regulatorio.Core.Mappers.Registros
{
    public class RegistroDtoProfile : Profile
    {
        public RegistroDtoProfile()
        {
            CreateMap<RegistroDto, RegistroResponse>()
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
