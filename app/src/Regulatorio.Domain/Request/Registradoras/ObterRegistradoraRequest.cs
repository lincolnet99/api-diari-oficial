using Microsoft.AspNetCore.Http;
using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Registradoras
{
    public class ObterRegistradorasRequest : BaseEntityRequest
    {
        public ObterRegistradorasRequest()
        {
            PageIndex = 0;
            PageSize = 10;
        }
        public int Id { get; set; }
        public string[] Uf { get; set; } = Array.Empty<string>();
        public string? Preco { get; set; }
        
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataTerminoVigencia { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }


        public string? NomeEmpresa { get; set; }
        public int? IdEmpresa { get; set; }

        public string? Portaria { get; set; }
        public string? UrlPortaria { get; set; }


        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; }

    }
}
