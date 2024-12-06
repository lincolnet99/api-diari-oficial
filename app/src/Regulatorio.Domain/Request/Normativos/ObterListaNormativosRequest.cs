using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Normativos
{
    public class ObterListaNormativosRequest : BaseEntityRequest
    {
        public ObterListaNormativosRequest()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public string[] Uf { get; set; } = Array.Empty<string>();
        public string? NomePortaria { get; set; }
        public string? NomeArquivo { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public DateTime? DataInicioCriacao { get; set; }
        public DateTime? DataFimCriacao { get; set; }
        public string[] Status { get; set; } = Array.Empty<string>();
        public string[] TipoRegistro { get; set; } = Array.Empty<string>();
        public string[] Tipo { get; set; } = Array.Empty<string>();
        public bool? VisaoNacional { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; }
    }
}
