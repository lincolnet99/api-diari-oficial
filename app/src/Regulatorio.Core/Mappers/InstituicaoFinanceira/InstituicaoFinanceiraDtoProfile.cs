using AutoMapper;
using Regulatorio.Domain.DTOs.InstituicaoFinanceira;
using Regulatorio.Domain.Response.InstituicaoFinanceira;

namespace Regulatorio.Core.Mappers.InstituicaoFinanceiras
{
    public class InstituicaoFinanceiraDtoProfile : Profile
    {
        public InstituicaoFinanceiraDtoProfile()
        {
            CreateMap<InstituicaoFinanceiraDto, InstituicaoFinanceiraResponse>()
               .ForMember(dest => dest.Documentos, opt => opt.MapFrom(src => src.Documentos))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
               .ForMember(dest => dest.PrecoCadastro, opt => opt.MapFrom(src => src.PrecoCadastro))
               .ForMember(dest => dest.PrecoRenovacaoCadastro, opt => opt.MapFrom(src => src.PrecoRenovacaoCadastro))
               .ForMember(dest => dest.Observacoes, opt => opt.MapFrom(src => src.Observacoes))
               .ForMember(dest => dest.Periodicidade, opt => opt.MapFrom(src => src.Periodicidade));

            CreateMap<InstituicaoFinanceiraDocumentosDto, InstituicaoFinanceiraDocumentosResponse>()
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdDocumento));




        }
    }
}
