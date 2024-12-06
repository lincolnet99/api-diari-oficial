using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.DTOs.Garantias;
using Regulatorio.Domain.Repositories.Garantias;
using Regulatorio.Domain.Request.Garantias;
using Regulatorio.SharedKernel.Common;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.Garantias
{
    public class GarantiaRepository : BaseRepository, IGarantiaRepository
    {
        public async Task<int> CriarGarantia(CriarGarantiaRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"INSERT INTO [Registro].[Garantia]
                               ([Uf]
                               ,[ValorPublico]
                               ,[ValorPrivado]
                               ,[TipoRegistro]
                               ,[ValorTotal]
                               ,[Observacao]
                               ,[CriadoEm])
		                       OUTPUT inserted.Id
                         VALUES
                               (@Uf,
                                @ValorPublico,
                                @ValorPrivado,
                                @TipoRegistro,
                                @ValorTotal,
                                @Observacao,
                                @CriadoEm)");

            #endregion

            var registro = await conn.QueryAsync<int>(sql.ToString(), new
            {
                request.Uf,
                request.ValorPublico,
                request.ValorPrivado,
                request.TipoRegistro,
                request.ValorTotal,
                request.Observacao,
                CriadoEm = DateTime.Now,
            });

            return registro.SingleOrDefault();
        }

        public async Task<GarantiaDto> EditarGarantia(int idGarantia, EditarGarantiaRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder("UPDATE Registro.Garantia SET ");

            if (!string.IsNullOrEmpty(request.Uf))
            {
                sql.Append("Uf = @Uf, ");
            }

            if (!string.IsNullOrEmpty(request.ValorPublico))
            {
                sql.Append("ValorPublico = @ValorPublico, ");
            }

            if (!string.IsNullOrEmpty(request.ValorPrivado))
            {
                sql.Append("ValorPrivado = @ValorPrivado, ");
            }

            if (!string.IsNullOrEmpty(request.TipoRegistro))
            {
                sql.Append("TipoRegistro = @TipoRegistro, ");
            }

            if (!string.IsNullOrEmpty(request.ValorTotal))
            {
                sql.Append("ValorTotal = @ValorTotal, ");
            }

            if (!string.IsNullOrEmpty(request.Observacao))
            {
                sql.Append("Observacao = @Observacao, ");
            }

            sql.Append("ModificadoEm = @ModificadoEm");

            if (sql.ToString()[^2..].ToString() == ", ")
            {
                sql.Remove(sql.Length - 2, 2);
            }

            sql.Append(" WHERE Id = @Id");

            await conn.ExecuteAsync(sql.ToString(), new
            {
                Id = idGarantia,
                request.Uf,
                request.ValorPublico,
                request.ValorPrivado,
                request.TipoRegistro,
                request.ValorTotal,
                request.Observacao,
                ModificadoEm = DateTime.Now
            });

            var response = await ObterGarantiaPorId(idGarantia);

            return response;
        }

        public async Task<int> ExcluirGarantia(int idGarantia)
        {
            await using var conn = new SqlConnection(ConnectionString);

            string sql = @"DELETE FROM Registro.Garantia WHERE Id = @IdGarantia";

            var result = await conn.QueryAsync(sql, new { IdGarantia = idGarantia });

            return idGarantia;
        }

        public async Task<GarantiaDto> ObterGarantiaPorUf(string uf)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT Id,
                                Uf,
                                ValorPublico,
	                            ValorPrivado,
	                            TipoRegistro,
	                            ValorTotal,
	                            Observacao,
	                            CriadoEm,
                                ModificadoEm
                          FROM Registro.Garantia WHERE Uf = @Uf");

            #endregion

            var garantia = await conn.QueryAsync<GarantiaDto>(sql.ToString(), new
            {
                Uf = uf
            });

            return garantia.FirstOrDefault();
        }

        public async Task<GarantiaDto> ObterGarantiaPorId(int id)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT Id,
                                Uf,
                                ValorPublico,
	                            ValorPrivado,
	                            TipoRegistro,
	                            ValorTotal,
	                            Observacao,
	                            CriadoEm,
                                ModificadoEm
                          FROM Registro.Garantia WHERE Id = @Id");

            #endregion

            var garantia = await conn.QueryAsync<GarantiaDto>(sql.ToString(), new
            {
                Id = id
            });

            return garantia.SingleOrDefault();
        }

        public async Task<PagedList<GarantiaDto>> ObterGarantias(ObterGarantiasRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<GarantiaDto>();

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT Id,
                                Uf,
                                ValorPublico,
	                            ValorPrivado,
	                            TipoRegistro,
	                            ValorTotal,
	                            Observacao,
	                            CriadoEm,
                                ModificadoEm
                          FROM Registro.Garantia WHERE 1 = 1");


            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            if (!string.IsNullOrEmpty(request.TipoRegistro))
                sql.Append(" AND TipoRegistro LIKE '%' + TRIM(@TipoRegistro) + '%'");

            if (string.IsNullOrEmpty(request.Sort))
            {
                sql.Append(@" ORDER BY CriadoEm DESC OFFSET @pageSize * @pageIndex ROWS
                                 FETCH NEXT @pageSize ROWS ONLY ");
            }
            else
            {
                var columnName = request.Sort.Split('.')[0];
                var order = request.Sort.Split('.')[1];

                sql.Append(@$" ORDER BY {columnName} {order.ToUpper()} OFFSET @pageSize * @pageIndex ROWS
                                 FETCH NEXT @pageSize ROWS ONLY ");
            }

            sql.AppendLine();

            sql.Append(@"SELECT COUNT(*) FROM Registro.Garantia WHERE 1 = 1");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            if (!string.IsNullOrEmpty(request.TipoRegistro))
                sql.Append(" AND TipoRegistro LIKE '%' + TRIM(@TipoRegistro) + '%'");

            #endregion

            var registros = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                request.Uf,
                request.TipoRegistro,
                request.PageIndex,
                request.PageSize,
                request.Sort
            });

            pagedResults.Items = registros.Read<GarantiaDto>();
            pagedResults.TotalCount = registros.ReadFirstOrDefault<int>();

            return pagedResults;
        }
    }
}
