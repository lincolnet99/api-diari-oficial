using Regulatorio.Core.Validators;
using Regulatorio.Core.Validators.Garantias;
using Regulatorio.Domain.Repositories.Garantias;
using Regulatorio.Domain.Request.Garantias;
using Regulatorio.Domain.Response.Garantias;
using Regulatorio.Domain.Services.Garantias;
using Regulatorio.SharedKernel;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.SharedKernel.Services;

namespace Regulatorio.ApplicationService.Services.Garantias
{
    public class GarantiaAppService : BaseService, IGarantiaService
    {
        private readonly IGarantiaRepository _garantiaRepository;

        public GarantiaAppService(IGarantiaRepository garantiaRepository)
        {
            _garantiaRepository = garantiaRepository;
        }

        public async Task<GarantiaResponse> EditarGarantia(int idGarantia, EditarGarantiaRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new GarantiaResponse();

            if (!string.IsNullOrEmpty(request.Uf))
            {
                var exists = await _garantiaRepository.ObterGarantiaPorUf(request.Uf);

                if (exists != null && exists?.Id != idGarantia)
                {
                    response.AddError("409", "Não é possível editar um novo registro porque já há um registro vinculado ao estado.", "uf");
                    return response;
                }
            }

            var registro = await _garantiaRepository.EditarGarantia(idGarantia, request);

            if (registro == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um novo registro");
                return response;
            }

            response.Id = idGarantia;

            response = registro.ToResponse<GarantiaResponse>();

            return response;
        }

        public async Task<CriarGarantiaResponse> CriarGarantia(CriarGarantiaRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarGarantiaResponse();

            var validator = new CriarGarantiasValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;

            var exists = await _garantiaRepository.ObterGarantiaPorUf(request.Uf);

            if (exists != null)
            {
                response.AddError("409", "Não é possível criar um novo registro porque já há um registro vinculado ao estado.");
                return response;
            }

            var registro = await _garantiaRepository.CriarGarantia(request);

            if (registro == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um novo registro");
                return response;
            }

            response.Id = registro;

            return response;
        }

        public async Task<ExcluirGarantiaResponse> ExcluirGarantia(int idGarantia)
        {
            idGarantia.Guard("The parameter can't be null for this operation", nameof(idGarantia));

            var response = new ExcluirGarantiaResponse();

            var registro = await _garantiaRepository.ExcluirGarantia(idGarantia);

            if (registro == null)
            {
                response.AddError("404", "Nenhum registro encontrado");
                return response;
            }

            return response;
        }

        public async Task<GarantiaResponse> ObterGarantiaPorUf(string uf)
        {
            uf.Guard("The parameter can't be null for this operation", nameof(uf));

            var response = new GarantiaResponse();

            var garantia = await _garantiaRepository.ObterGarantiaPorUf(uf);

            if (garantia == null)
            {
                response.AddError("404", "Nenhuma garantia encontrada");
                return response;
            }

            response = garantia.ToResponse<GarantiaResponse>();

            return response;
        }

        public async Task<GarantiaResponse> ObterGarantiaPorId(int id)
        {
            id.Guard("The parameter can't be null for this operation", nameof(id));

            var response = new GarantiaResponse();

            var registro = await _garantiaRepository.ObterGarantiaPorId(id);

            if (registro == null)
            {
                response.AddError("404", "Nenhum registro encontrada");
                return response;
            }

            response = registro.ToResponse<GarantiaResponse>();

            return response;
        }

        public async Task<ObterGarantiasResponse> ObterGarantias(ObterGarantiasRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new ObterGarantiasResponse();

            var garantias = await _garantiaRepository.ObterGarantias(request);

            if (garantias == null)
            {
                response.AddError("404", "Nenhuma garantia encontrada");
                return response;
            }

            foreach (var normativo in garantias.Items)
                response.Garantias.Add(normativo.ToResponse<GarantiaResponse>());

            response.PageIndex = request.PageIndex;
            response.TotalItems = garantias.TotalCount;

            return response;
        }
    }
}
