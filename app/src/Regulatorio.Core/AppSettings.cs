using Regulatorio.Domain.Entities.Normativos;
using System.Collections.Generic;

namespace Regulatorio.Core
{
    public class AppSettings
    {
        public string RegulatorioDatabaseServer { get; set; }
        public string NormativoContainerConnectionString { get; set; }
    }
}