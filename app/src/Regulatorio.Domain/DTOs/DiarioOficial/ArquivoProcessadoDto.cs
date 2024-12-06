using Regulatorio.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulatorio.Domain.DTOs.DiarioOficial
{
    public class ArquivoProcessadoDto : BaseEntity
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
