using Regulatorio.Core.Validators;
using Regulatorio.Core.Validators.Registradora;
using Regulatorio.Domain.AzureStorage;
using Regulatorio.Domain.Repositories.RegistradoraRepository;
using Regulatorio.Domain.Request.Registradoras;
using Regulatorio.Domain.Response.Registradoras;
using Regulatorio.Domain.Services.Registradoras;
using Regulatorio.SharedKernel;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.SharedKernel.Services;
using System;

namespace Regulatorio.ApplicationService.Services.Registradoras
{
    public class RegistradorasAppService : BaseService, IRegistradorasService
    {
        private IRegistradoraRepository _registradorasRepository;
        private readonly IFileStorageRepository _fileStorageRepository;


        public RegistradorasAppService(IRegistradoraRepository registradorasRepository, IFileStorageRepository fileStorageRepository)
        {
            _registradorasRepository = registradorasRepository;
            _fileStorageRepository = fileStorageRepository;
        }

        public async Task<RegistradorasResponse> EditarRegistradora(int id, EditarRegistradoraRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new RegistradorasResponse();

            if (id == 0)
                response.AddError("400", "campo Id obrigatório");

            var exists = await _registradorasRepository.ObterRegistradoraPorId(id);
            if (exists is null)
                response.AddError("400", "Registradora não encontrada");

            request.Id = id;
            
            string url = "";
            var fileName = "";

            if (request.Portaria != null && request.Portaria.Length > 0)
            {
                var extensaoArquivo = Path.GetExtension(request.Portaria.FileName);
                using (var ms = new MemoryStream())
                {
                    await request.Portaria.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    url = await _fileStorageRepository.UploadFileAsync($"{Guid.NewGuid()}{extensaoArquivo}", ms);
                }
                fileName = Path.GetFileNameWithoutExtension(request.Portaria.FileName);
            }
            

            var registradora = await _registradorasRepository.EditarRegistradora(request, fileName, url);

            response.Id = registradora.Id;


            if (registradora == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um nova Registradora");
                return response;
            }

            response = registradora.ToResponse<RegistradorasResponse>();

            return response;
        }

        public async Task<CriarRegistradorasResponse> CriarRegistradora(CriarRegistradoraRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarRegistradorasResponse();
            var validator = new CriarRegistradoraValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;

            var registradora = await _registradorasRepository.CriarRegistradora(request);

            if (registradora.Items == null)
            {
                response.AddError("500", $"Ocorreu um erro ao criar uma nova Registradora.");
                return response;
            }

            var extensaoArquivo = Path.GetExtension(request.Portaria.FileName);

            using (var ms = new MemoryStream())
            {
                await request.Portaria.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                string url = await _fileStorageRepository.UploadFileAsync($"{Guid.NewGuid()}{extensaoArquivo}", ms);

                await _registradorasRepository.EditarArquivoRegistradora(url, registradora.Items.FirstOrDefault().Id, Path.GetFileNameWithoutExtension(request.Portaria.FileName));
            }

            response.Id = registradora.Items.FirstOrDefault().Id;

            return response;
        }

        public async Task<ExcluirRegistradorasResponse> ExcluirRegistradora(int idRegistradoras)
        {
            idRegistradoras.Guard("The parameter can't be null for this operation", nameof(idRegistradoras));

            var response = new ExcluirRegistradorasResponse();

            var registradoras = await _registradorasRepository.ExcluirRegistradora(idRegistradoras);

            if (registradoras == 0)
            {
                response.AddError("404", "Nenhuma Registradora encontrado");
                return response;
            }

            return response;
        }

        public async Task<ObterRegistradorasPorUfResponse> ObterRegistradoraPorUf(string uf)
        {

            uf.Guard("The parameter can't be null for this operation", nameof(uf));
            var response = new ObterRegistradorasPorUfResponse();

            var registradoras = await _registradorasRepository.ObterRegistradoraPorUf(uf);

            if (registradoras == null || !registradoras.Items.Any())
            {
                response.AddError("404", "Nenhuma registradora encontrada");
                return response;
            }

            var groupedRegistradoras = registradoras.Items
                .GroupBy(r => r.Empresas.Nome)
                .Select(g => new RegistradorasPorUfResponse
                {
                    Empresa = g.Key,
                    Ufs = String.Join(", ", g.Select(r => r.Uf).Distinct())
                }).ToList();

            response.Registradoras = groupedRegistradoras;

            response.TotalItems = registradoras.TotalCount;

            return response;
        }

        public async Task<RegistradorasResponse> ObterRegistradoraPorId(int id)
        {
            id.Guard("The parameter can't be null for this operation", nameof(id));

            var response = new RegistradorasResponse();

            var registradoras = await _registradorasRepository.ObterRegistradoraPorId(id);

            if (registradoras == null)
            {
                response.AddError("404", "Nenhuma Registradora encontrada");
                return response;
            }

            response = registradoras.ToResponse<RegistradorasResponse>();

            return response;
        }

        public async Task<ObterRegistradorasResponse> ObterRegistradoras(ObterRegistradorasRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new ObterRegistradorasResponse();

            var registradoras = await _registradorasRepository.ObterRegistradoras(request);

            if (registradoras == null)
            {
                response.AddError("404", "Nenhuma registradora encontrada");
                return response;
            }

            foreach (var registradora in registradoras.Items)
                response.Registradoras.Add(registradora.ToResponse<RegistradorasResponse>());

            response.PageIndex = request.PageIndex;
            response.TotalItems = registradoras.TotalCount;

            return response;
        }

        public async Task<ObterRegistradorasPorEmpresaResponse> ObterRegistradoraPorEmpresa(string empresa)
        {

            empresa.Guard("The parameter can't be null for this operation", nameof(empresa));
            var response = new ObterRegistradorasPorEmpresaResponse();

            var registradoras = await _registradorasRepository.ObterRegistradoraPorEmpresa(empresa);

            if (registradoras == null || !registradoras.Items.Any())
            {
                response.AddError("404", "Nenhuma registradora encontrada");
                return response;
            }

            var groupedRegistradoras = registradoras.Items
                .GroupBy(r => r.Empresas.Nome)
                .Select(g => new RegistradorasPorEmpresaResponse
                {
                    Empresa = g.Key,
                    Ufs = String.Join(", ", g.Select(r => r.Uf).ToList())
                }).ToList();

            response.Registradoras = groupedRegistradoras;

            response.TotalItems = registradoras.TotalCount;

            return response;
        }

        public async Task<ObterRegistradoraParaDownloadResponse> ObterRegistradoraParaDownload(int idRegistradora)
        {
            idRegistradora.Guard("The parameter can't be null for this operation", nameof(idRegistradora));

            var response = new ObterRegistradoraParaDownloadResponse();

            var Registradora = await _registradorasRepository.ObterRegistradoraParaDownload(idRegistradora);

            if (Registradora == null)
            {
                response.AddError("404", "Nenhum Registradora foi localizada");
                return response;
            }

            response.NomeArquivo = Registradora.NomeArquivo;

            UriBuilder uriBuilder = new(Registradora.UrlArquivo)
            {
                Query = string.Empty
            };

            if (!string.IsNullOrEmpty(Path.GetFileName(uriBuilder.ToString())))
                response.Arquivo = await _fileStorageRepository.GetFileAsync(Path.GetFileName(uriBuilder.ToString()));

            return response;
        }
    }
}
