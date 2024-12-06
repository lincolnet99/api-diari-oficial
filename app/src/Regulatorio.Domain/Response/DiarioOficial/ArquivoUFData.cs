using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulatorio.Domain.Response.DiarioOficial
{
    public class ArquivoUFData
    {
        public string Estado { get; set; }
        public DateTime DataArquivo { get; set; }
        public string LinkPagina { get; set; }
    }
}
