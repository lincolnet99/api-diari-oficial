using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulatorio.Domain.Response.DiarioOficial
{
    public class DadosDocumentos
    {
        public int TotalDocumentos { get; set; }
        public List<PalavraChavePorEstado> PalavrasChavePorEstado { get; set; }
    }

    public class PalavraChavePorEstado
    {
        public string Estado { get; set; }
        public string PalavraChave { get; set; }
        public int Contagem { get; set; }
    }
}
