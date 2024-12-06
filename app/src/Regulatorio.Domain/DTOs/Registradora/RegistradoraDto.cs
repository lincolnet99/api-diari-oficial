using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.Registradora
{
    public class RegistradoraDto : BaseEntity
    {
        public string? Uf { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public DateTime? DataTerminoVigencia { get; set; }
        public string? Preco { get; set; }
        public string? Portaria { get; set; }
        public string? UrlPortaria { get; set; }
        public DateTime DataCriacaoRegistradora { get; set; }
        public DateTime DataModificacaoRegistradora { get; set; }
        public RegistradoraEmpresaDto Empresas { get; set; }
    }

    public class RegistradoraEmpresaDto
    {
        public int IdEmpresa { get; set; }
        public string? Nome { get; set; }
        public DateTime DataCriacaoEmpresa { get; set; }
        public DateTime DataModificacaoEmpresa { get; set; }

    }
}
