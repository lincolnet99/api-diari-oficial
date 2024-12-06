using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registradoras
{
    public class CriarRegistradorasResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? Uf { get; set; }
        public string? DataInicioVigencia { get; set; }
        public string? DataTerminoVigencia { get; set; }
        public string? Preco { get; set; }

        public CriarRegistradoraEmpresaResponse Empresa { get; set; }
    }

    public class CriarRegistradoraEmpresaResponse
    {
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
    }
}
