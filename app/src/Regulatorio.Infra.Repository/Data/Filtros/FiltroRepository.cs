using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.Entities.Filtros;
using Regulatorio.Domain.Repositories.Filtros;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.Filtros
{
    public class FiltroRepository : BaseRepository, IFiltroRepository
    {
        public async Task<IEnumerable<TipoNormativo>> ObterTipoNormativo()
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT [Id]
                              ,[Nome]
                              ,[Descricao]
                              ,[CriadoEm]
                          FROM [Filtro].[TipoNormativo]");

            #endregion

            var tiposNormativo = await conn.QueryAsync<TipoNormativo>(sql.ToString());

            return tiposNormativo;
        }
    }
}
