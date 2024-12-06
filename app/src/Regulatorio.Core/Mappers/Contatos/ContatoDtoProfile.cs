using AutoMapper;
using Regulatorio.Domain.DTOs.Contatos;
using Regulatorio.Domain.Response.Contatos;

namespace Regulatorio.Core.Mappers.Contatos
{
    public class ContatoDtoProfile : Profile
    {
        public ContatoDtoProfile()
        {
            CreateMap<ContatoDto, ContatoResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
               .ForMember(dest => dest.Orgao, opt => opt.MapFrom(src => src.Orgao))
               .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
               .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}




