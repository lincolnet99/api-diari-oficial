using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulatorio.Domain.Request.DiarioOficial
{
    public class ConsultarListagemArquivosProcessadosRequest
    {
        public ConsultarListagemArquivosProcessadosRequest()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public string[] Estado { get; set; } = Array.Empty<string>();
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public bool? Relevante { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; }
    }
}
