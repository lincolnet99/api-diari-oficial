using Regulatorio.Domain.Response.Contatos;
using Regulatorio.SharedKernel.Entities;
using Regulatorio.SharedKernel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulatorio.Domain.Response.DiarioOficial
{

    public class ListaArquivosProcessadosResponse : BaseResponse
    {
        public ListaArquivosProcessadosResponse()
        {
            ListaArquivoProcessado = new List<ArquivoProcessadoResponse>();
        }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<ArquivoProcessadoResponse> ListaArquivoProcessado { get; set; }
    }

    public class ArquivoProcessadoResponse : BaseResponse
    {
        public int Id { get; set; }
        public string LinkPagina { get; set; }
        public string PalavraChave { get; set; }
        public string NomeArquivo { get; set; }
        public string ResumoArquivo { get; set; }
        public DateTime DataArquivo { get; set; }
        public string? LinkDownload { get; set; }
        public string Estado { get; set; }
        public bool Relevante { get; set; }
    }
}
