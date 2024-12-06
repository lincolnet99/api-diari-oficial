using Regulatorio.Core.Validators;
using Regulatorio.Core.Validators.Registros;
using Regulatorio.Domain.Repositories.Registros;
using Regulatorio.Domain.Request.Registros;
using Regulatorio.Domain.Response.Registros;
using Regulatorio.Domain.Services.Registros;
using Regulatorio.SharedKernel;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.SharedKernel.Services;

namespace Regulatorio.ApplicationService.Services.Registros
{
    public class RegistroAppService : BaseService, IRegistroService
    {
        private IRegistroRepository _registroRepository;

        public RegistroAppService(IRegistroRepository registroRepository)
        {
            _registroRepository = registroRepository;
        }

        public async Task<RegistroResponse> EditarRegistro(int idRegistro, EditarRegistroRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new RegistroResponse();

            if (!string.IsNullOrEmpty(request.Uf))
            {
                var exists = await _registroRepository.ObterRegistroPorUf(request.Uf);

                if (exists != null && exists?.Id != idRegistro)
                {
                    response.AddError("409", "Não é possível editar um novo registro porque já há um registro vinculado ao estado.", "uf");
                    return response;
                }
            }

            var registro = await _registroRepository.EditarRegistro(idRegistro, request);

            if (registro == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um novo registro");
                return response;
            }

            response.Id = idRegistro;

            response = registro.ToResponse<RegistroResponse>();

            return response;
        }

        public async Task<CriarRegistroResponse> CriarRegistro(CriarRegistroRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarRegistroResponse();
            var validator = new CriarRegistrosValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;
            var exists = await _registroRepository.ObterRegistroPorUf(request.Uf);

            if (exists != null)
            {
                response.AddError("409", "Não é possível criar um novo registro porque já há um registro vinculado ao estado.");
                return response;
            }

            var registro = await _registroRepository.CriarRegistro(request);

            if (registro == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um novo registro");
                return response;
            }

            response.Id = registro;

            return response;
        }

        public async Task<ExcluirRegistroResponse> ExcluirRegistro(int idRegistro)
        {
            idRegistro.Guard("The parameter can't be null for this operation", nameof(idRegistro));

            var response = new ExcluirRegistroResponse();

            var registro = await _registroRepository.ExcluirRegistro(idRegistro);

            if (registro == null)
            {
                response.AddError("404", "Nenhum registro encontrado");
                return response;
            }

            return response;
        }

        public async Task<RegistroResponse> ObterRegistroPorUf(string uf)
        {
            uf.Guard("The parameter can't be null for this operation", nameof(uf));

            var response = new RegistroResponse();

            var registro = await _registroRepository.ObterRegistroPorUf(uf);

            if (registro == null)
            {
                response.AddError("404", "Nenhum registro encontrada");
                return response;
            }

            response = registro.ToResponse<RegistroResponse>();

            return response;
        }

        public async Task<RegistroResponse> ObterRegistroPorId(int id)
        {
            id.Guard("The parameter can't be null for this operation", nameof(id));

            var response = new RegistroResponse();

            var registro = await _registroRepository.ObterRegistroPorId(id);

            if (registro == null)
            {
                response.AddError("404", "Nenhum registro encontrada");
                return response;
            }

            response = registro.ToResponse<RegistroResponse>();

            return response;
        }

        public async Task<ObterRegistrosResponse> ObterRegistros(ObterRegistrosRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new ObterRegistrosResponse();

            var garantias = await _registroRepository.ObterRegistros(request);

            if (garantias == null)
            {
                response.AddError("404", "Nenhum registro encontrado");
                return response;
            }

            foreach (var normativo in garantias.Items)
                response.Registros.Add(normativo.ToResponse<RegistroResponse>());

            response.PageIndex = request.PageIndex;
            response.TotalItems = garantias.TotalCount;

            return response;
        }
    }
}
