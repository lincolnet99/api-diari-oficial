using Regulatorio.Core.Validators;
using Regulatorio.Core.Validators.Normativos;
using Regulatorio.Domain.AzureStorage;
using Regulatorio.Domain.Repositories.Normativos;
using Regulatorio.Domain.Request.Normativos;
using Regulatorio.Domain.Response.Normativos;
using Regulatorio.Domain.Services.Normativos;
using Regulatorio.SharedKernel;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.SharedKernel.Services;

namespace Regulatorio.ApplicationService.Services.Normativos
{
    public class NormativoAppService : BaseService, INormativoService
    {
        private readonly INormativoRepository _normativoRepository;
        private readonly IFileStorageRepository _fileStorageRepository;

        public NormativoAppService(INormativoRepository normativoRepository,
            IFileStorageRepository fileStorageRepository)
        {
            _normativoRepository = normativoRepository;
            _fileStorageRepository = fileStorageRepository;
        }

        public async Task<NormativoResponse> EditarNormativo(int idNormativo, EditarNormativoRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new NormativoResponse();
            var validator = new EditarNormativosValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);
            var normativo = await _normativoRepository.EditarNormativo(idNormativo, request);

            if (normativo == null)
            {
                response.AddError("500", "Erro ao editar normativo");
                return response;
            }

            if(request.Arquivo != null)
            {
                var extensaoArquivo = Path.GetExtension(request.Arquivo.FileName);

                using var ms = new MemoryStream();

                await request.Arquivo.CopyToAsync(ms);

                ms.Seek(0, SeekOrigin.Begin);

                await _fileStorageRepository.DeleteFileAsync(Path.GetFileName(normativo.UrlArquivo));

                string url = await _fileStorageRepository.UploadFileAsync($"{Guid.NewGuid()}{extensaoArquivo}", ms);

                await _normativoRepository.EditarArquivoNormativo(url, idNormativo);
            }

            response = normativo.ToResponse<NormativoResponse>();

            return response;
        }

        public async Task<AtivarArquivarNormativoResponse> AtivarArquivarNormativo(int idNormativo)
        {
            idNormativo.Guard("The parameter can't be null for this operation", nameof(idNormativo));

            var response = new AtivarArquivarNormativoResponse();

            var normativo = await _normativoRepository.AtivarArquivarNormativo(idNormativo);

            if (normativo == null)
            {
                response.AddError("500", "Erro ao ativar/arquivar normativo");
                return response;
            }

            response = normativo.ToResponse<AtivarArquivarNormativoResponse>();

            return response;
        }

        public async Task<CriarNormativoResponse> CriarNormativo(CriarNormativoRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarNormativoResponse();
            var validator = new CriarNormativoValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;

            var normativo = await _normativoRepository.CriarNormativo(request);

            if (normativo == null || normativo == 0)
            {
                response.AddError("500", "Erro ao cadastrar normativo");
                return response;
            }

            var extensaoArquivo = Path.GetExtension(request.Arquivo.FileName);

            using (var ms = new MemoryStream())
            {
                await request.Arquivo.CopyToAsync(ms);

                ms.Seek(0, SeekOrigin.Begin);

                string url = await _fileStorageRepository.UploadFileAsync($"{Guid.NewGuid()}{extensaoArquivo}", ms);

                await _normativoRepository.EditarArquivoNormativo(url, normativo);
            }

            response.Id = normativo;

            return response;
        }

        public async Task<ExcluirNormativoResponse> ExcluirNormativo(int idNormativo)
        {
            idNormativo.Guard("The parameter can't be null for this operation", nameof(idNormativo));

            var response = new ExcluirNormativoResponse();

            var normativo = await _normativoRepository.ObterNormativoPorId(idNormativo);

            if (normativo == null)
            {
                response.AddError("404", "Nenhum normativo encontrado");
                return response;
            }

            await _normativoRepository.ExcluirNormativo(idNormativo);

            await _fileStorageRepository.DeleteFileAsync(Path.GetFileName(normativo.UrlArquivo));

            response.Id = idNormativo;

            return response;
        }

        public async Task<NormativoResponse> ObterNormativoPorId(int idNormativo)
        {
            idNormativo.Guard("The parameter can't be null for this operation", nameof(idNormativo));

            var response = new NormativoResponse();

            var normativo = await _normativoRepository.ObterNormativoPorId(idNormativo);

            if (normativo == null)
            {
                response.AddError("404", "Nenhum normativo encontrado");
                return response;
            }

            response = normativo.ToResponse<NormativoResponse>();

            return response;
        }

        public async Task<ObterNormativosResponse> ObterNormativos(ObterListaNormativosRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new ObterNormativosResponse();

            var normativos = await _normativoRepository.ObterNormativos(request);

            if (normativos == null)
            {
                response.AddError("404", "Nenhum normativo encontrado");
                return response;
            }

            foreach (var normativo in normativos.Items)
                response.Normativos.Add(normativo.ToResponse<NormativoResponse>());

            response.PageIndex = request.PageIndex;
            response.TotalItems = normativos.TotalCount;

            return response;
        }

        public async Task<ObterNormativoParaDownloadResponse> ObterNormativoParaDownload(int idNormativo)
        {
            idNormativo.Guard("The parameter can't be null for this operation", nameof(idNormativo));

            var response = new ObterNormativoParaDownloadResponse();

            var normativo = await _normativoRepository.ObterNormativoParaDownload(idNormativo);

            if (normativo == null)
            {
                response.AddError("404", "Nenhum normativo encontrado");
                return response;
            }

            response.NomeArquivo = normativo.NomeArquivo;

            UriBuilder uriBuilder = new(normativo.UrlArquivo)
            {
                Query = string.Empty
            };

            if (!string.IsNullOrEmpty(Path.GetFileName(uriBuilder.ToString())))
                response.Arquivo = await _fileStorageRepository.GetFileAsync(Path.GetFileName(uriBuilder.ToString()));

            return response;
        }

        public async Task<ObterUfsRecentesResponse> ObterUfsRecentes()
        {
            var response = new ObterUfsRecentesResponse();

            var ufsRecentes = await _normativoRepository.ObterUfsRecentes();

            foreach (var valor in ufsRecentes)
                response.Ufs.Add(valor.Uf);

            return response;
        }
    }
}
