using AutoMapper;
using Regulatorio.Domain.DTOs.DiarioOficial;
using Regulatorio.Domain.DTOs.PalavrasChave;
using Regulatorio.Domain.Response.DiarioOficial;
using Regulatorio.Domain.Response.PalavrasChave;

namespace Regulatorio.Core.Mappers.PalavrasChave
{
    public class DiarioOficialDtoProfile : Profile
    {
        public DiarioOficialDtoProfile()
        {
			CreateMap<ListaPalavraChaveDto, ListaPalavraChaveResponse>()
				   .ForMember(dest => dest.ListaPalavras, opt => opt.MapFrom(src => src.ListaPalavras));

			CreateMap<PalavraChaveDto, PalavraChaveResponse>()
				  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				  .ForMember(dest => dest.Palavra, opt => opt.MapFrom(src => src.Palavra))
				  .ForMember(dest => dest.CriadaPor, opt => opt.MapFrom(src => src.CriadaPor))
				  .ForMember(dest => dest.CriadaEm, opt => opt.MapFrom(src => src.CriadaEm))
				  .ForMember(dest => dest.Ativa, opt => opt.MapFrom(src => src.Ativa));
			
			CreateMap<ListaArquivosProcessadosDto, ListaArquivosProcessadosResponse>()
				   .ForMember(dest => dest.ListaArquivoProcessado, opt => opt.MapFrom(src => src.ListaArquivoProcessados));

			CreateMap<ArquivoProcessadoDto, ArquivoProcessadoResponse>()
				  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				  .ForMember(dest => dest.LinkPagina, opt => opt.MapFrom(src => src.LinkPagina))
				  .ForMember(dest => dest.PalavraChave, opt => opt.MapFrom(src => src.PalavraChave))
				  .ForMember(dest => dest.NomeArquivo, opt => opt.MapFrom(src => src.NomeArquivo))
				  .ForMember(dest => dest.ResumoArquivo, opt => opt.MapFrom(src => src.ResumoArquivo))
				  .ForMember(dest => dest.DataArquivo, opt => opt.MapFrom(src => src.DataArquivo))
				  .ForMember(dest => dest.LinkDownload, opt => opt.MapFrom(src => src.LinkDownload))
				  .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
				  .ForMember(dest => dest.Relevante, opt => opt.MapFrom(src => src.Relevante));
		}
    }
}
