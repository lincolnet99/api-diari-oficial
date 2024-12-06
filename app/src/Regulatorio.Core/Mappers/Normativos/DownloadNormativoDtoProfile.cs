using AutoMapper;
using Regulatorio.Domain.DTOs.Normativos;
using Regulatorio.Domain.Response.Normativos;

namespace Regulatorio.Core.Mappers.Normativos
{
    public class DownloadNormativoDtoProfile : Profile
    {
        public DownloadNormativoDtoProfile()
        {
            CreateMap<DownloadNormativoDto, ObterNormativoParaDownloadResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomeArquivo, opt => opt.MapFrom(src => src.NomeArquivo));
        }
    }
}
