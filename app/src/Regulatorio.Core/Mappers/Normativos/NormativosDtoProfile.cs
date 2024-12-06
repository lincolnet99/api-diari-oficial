using AutoMapper;
using Regulatorio.Domain.DTOs.Normativos;
using Regulatorio.Domain.Response.Normativos;

namespace Regulatorio.Core.Mappers.Normativos
{
    public class NormativosDtoProfile : Profile
    {
        public NormativosDtoProfile()
        {
            CreateMap<NormativoDto, NormativoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
                .ForMember(dest => dest.NomePortaria, opt => opt.MapFrom(src => src.NomePortaria))
                .ForMember(dest => dest.NomeArquivo, opt => opt.MapFrom(src => src.NomeArquivo))
                .ForMember(dest => dest.DataVigencia, opt => opt.MapFrom(src => src.DataVigencia))
                .ForMember(dest => dest.CriadoEm, opt => opt.MapFrom(src => src.CriadoEm))
                .ForMember(dest => dest.TipoNormativo, opt => opt.MapFrom(src => src.TipoNormativo))
                .ForMember(dest => dest.TipoRegistro, opt => opt.MapFrom(src => src.TipoRegistro))
                .ForMember(dest => dest.EhVisaoNacional, opt => opt.MapFrom(src => src.EhVisaoNacional))
                .ForMember(dest => dest.ModificadoEm, opt => opt.MapFrom(src => src.ModificadoEm))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
