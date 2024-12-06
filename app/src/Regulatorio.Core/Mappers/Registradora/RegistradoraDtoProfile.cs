using AutoMapper;
using Regulatorio.Domain.DTOs.Registradora;
using Regulatorio.Domain.Response.Registradoras;

namespace Regulatorio.Core.Mappers.Registradora
{
    public class RegistradoraDtoProfile : Profile
    {
        public RegistradoraDtoProfile()
        {
            CreateMap<RegistradoraDto, RegistradorasResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
                .ForMember(dest => dest.DataInicioVigencia, opt => opt.MapFrom(src => src.DataInicioVigencia))
                .ForMember(dest => dest.DataTerminoVigencia, opt => opt.MapFrom(src => src.DataTerminoVigencia))
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco))
                .ForMember(dest => dest.Empresas, opt => opt.MapFrom(src => src.Empresas));


            CreateMap<RegistradoraEmpresaDto, RegistradorasEmpresaResponse>();
            CreateMap<RegistradoraDto, RegistradorasPorUfResponse>();
            CreateMap<RegistradoraDto, RegistradorasPorEmpresaResponse>();

        }
    }
}
