using AutoMapper;
using Regulatorio.Domain.DTOs.Normativos;
using Regulatorio.Domain.Response.Normativos;

namespace Regulatorio.Core.Mappers.Normativos
{
    public class AtivarArquivarNormativoDtoProfile : Profile
    {
        public AtivarArquivarNormativoDtoProfile()
        {
            CreateMap<AtivarArquivarNormativoDto, AtivarArquivarNormativoResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StatusAtual, opt => opt.MapFrom(src => src.Status));
        }
    }
}
