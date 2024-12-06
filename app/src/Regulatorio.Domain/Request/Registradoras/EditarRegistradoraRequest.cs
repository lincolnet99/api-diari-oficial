using Microsoft.AspNetCore.Http;
using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Registradoras
{
    public class EditarRegistradoraRequest : BaseEntityRequest
    {
        public int Id { get; set; }
        public string? Uf { get; set; }
        public string? Preco { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataTerminoVigencia { get; set; }
        public IFormFile? Portaria { get; set; }
        public string? NomeEmpresa { get; set; }
        public int? IdEmpresa { get; set; }
    }

    //public class EditarRegistradoraEmpresasRequest
    //{
    //    public int Id { get; set; }
    //    public string? Nome { get; set; }

    //}
}
