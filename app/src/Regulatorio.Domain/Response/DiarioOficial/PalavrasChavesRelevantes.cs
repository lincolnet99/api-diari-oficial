using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulatorio.Domain.Response.DiarioOficial
{
    public class PalavrasChavesRelevantes
    {
        public List<PalavraChave> ListaPalavras { get; set; } = new List<PalavraChave>();
    }

    public class PalavraChave
    {
        public int Quantidade { get; set; }
        public string? Palavra { get; set; } 
    }
}
