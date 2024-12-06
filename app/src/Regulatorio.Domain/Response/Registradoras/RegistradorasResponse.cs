using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registradoras
{
    public class RegistradorasResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? Uf { get; set; }
        public string? DataInicioVigencia { get; set; }
        public string? DataTerminoVigencia { get; set; }

        public string? DataCriacao { get; set; }
        public string? DataModificacao { get; set; }
        public string? Preco { get; set; }

        public string? Portaria { get; set; }
        public string? UrlPortaria { get; set; }

        public RegistradorasEmpresaResponse Empresas { get; set; }
    }

    public class RegistradorasEmpresaResponse
    {
        public string IdEmpresa { get; set; }
        public string Nome { get; set; }
    }
}