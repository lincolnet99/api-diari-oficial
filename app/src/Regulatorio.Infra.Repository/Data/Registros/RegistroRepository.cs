using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.DTOs.Registros;
using Regulatorio.Domain.Repositories.Registros;
using Regulatorio.Domain.Request.Registros;
using Regulatorio.SharedKernel.Common;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.Registros
{
    public class RegistroRepository : BaseRepository, IRegistroRepository
    {
        public async Task<int> CriarRegistro(CriarRegistroRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"INSERT INTO [Registro].[Contrato]
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

        public async Task<RegistroDto> EditarRegistro(int idRegistro, EditarRegistroRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder("UPDATE Registro.Contrato SET ");

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
                Id = idRegistro,
                request.Uf,
                request.ValorPublico,
                request.ValorPrivado,
                request.TipoRegistro,
                request.ValorTotal,
                request.Observacao,
                ModificadoEm = DateTime.Now
            });

            var response = await ObterRegistroPorId(idRegistro);

            return response;
        }

        public async Task<int> ExcluirRegistro(int idRegistro)
        {
            await using var conn = new SqlConnection(ConnectionString);

            string sql = @"DELETE FROM Registro.Contrato WHERE Id = @IdRegistro";

            var result = await conn.QueryAsync(sql, new { IdRegistro = idRegistro });

            return idRegistro;
        }

        public async Task<RegistroDto> ObterRegistroPorUf(string uf)
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
                          FROM Registro.Contrato WHERE Uf = @Uf");

            #endregion

            var garantia = await conn.QueryAsync<RegistroDto>(sql.ToString(), new
            {
                Uf = uf
            });

            return garantia.FirstOrDefault();
        }

        public async Task<RegistroDto> ObterRegistroPorId(int id)
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
                          FROM Registro.Contrato WHERE Id = @Id");

            #endregion

            var garantia = await conn.QueryAsync<RegistroDto>(sql.ToString(), new
            {
                Id = id
            });

            return garantia.SingleOrDefault();
        }

        public async Task<PagedList<RegistroDto>> ObterRegistros(ObterRegistrosRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<RegistroDto>();

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
                          FROM Registro.Contrato WHERE 1 = 1");

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

            sql.Append(@"SELECT COUNT(*) FROM Registro.Contrato WHERE 1 = 1");

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

            pagedResults.Items = registros.Read<RegistroDto>();
            pagedResults.TotalCount = registros.ReadFirstOrDefault<int>();

            return pagedResults;
        }
    }
}
