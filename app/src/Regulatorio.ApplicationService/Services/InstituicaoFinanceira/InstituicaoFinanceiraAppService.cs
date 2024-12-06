using Regulatorio.Core.Validators;
using Regulatorio.Core.Validators.InstituicaoFinanceira;
using Regulatorio.Domain.Repositories.InstituicaoFinanceiraRepository;
using Regulatorio.Domain.Request.InstituicaoFinanceira;
using Regulatorio.Domain.Response.InstituicaoFinanceira;
using Regulatorio.Domain.Services.InstituicaoFinanceira;
using Regulatorio.SharedKernel;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.SharedKernel.Services;

namespace Regulatorio.ApplicationService.Services.InstituicaoFinanceiras
{
    public class InstituicaoFinanceiraAppService : BaseService, IInstituicaoFinanceiraService
    {
        private IInstituicaoFinanceiraRepository _instituicaoFinanceiraRepository;

        public InstituicaoFinanceiraAppService(IInstituicaoFinanceiraRepository InstituicaoFinanceiraRepository)
        {
            _instituicaoFinanceiraRepository = InstituicaoFinanceiraRepository;
        }

        public async Task<InstituicaoFinanceiraResponse> EditarInstituicaoFinanceira(EditarInstituicaoFinanceiraRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new InstituicaoFinanceiraResponse();

            if (request.Id == 0)
                response.AddError("400", "campo Id obrigatório");

            var exists = await _instituicaoFinanceiraRepository.ObterInstituicaoFinanceiraPorId(request.Id);
            if (exists is null)
                response.AddError("400", "Instituição financeira não encontrada");


            var instituicaoFinanceira = await _instituicaoFinanceiraRepository.EditarInstituicaoFinanceira(request);

            if (instituicaoFinanceira == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um novo InstituicaoFinanceira");
                return response;
            }

            await _instituicaoFinanceiraRepository.EditarInstituicaoFinanceiraDocumentos(request);


            response = instituicaoFinanceira.ToResponse<InstituicaoFinanceiraResponse>();

            return response;
        }

        public async Task<CriarInstituicaoFinanceiraResponse> CriarInstituicaoFinanceira(CriarInstituicaoFinanceiraRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarInstituicaoFinanceiraResponse();
            var validator = new CriarInstituicaoFinanceiraValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;

            var instituicaoFinanceira = await _instituicaoFinanceiraRepository.CriarInstituicaoFinanceira(request);

            if (instituicaoFinanceira == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um novo InstituicaoFinanceira");
                return response;
            }

            if (request.Documentos.Count > 0)
                await _instituicaoFinanceiraRepository.CriarInstituicaoFinanceiraDocumentos(request.Documentos, instituicaoFinanceira);

            response.Id = instituicaoFinanceira;

            return response;
        }

        public async Task<ExcluirInstituicaoFinanceiraResponse> ExcluirInstituicaoFinanceira(int idInstituicaoFinanceira)
        {
            idInstituicaoFinanceira.Guard("The parameter can't be null for this operation", nameof(idInstituicaoFinanceira));

            var response = new ExcluirInstituicaoFinanceiraResponse();

            var instituicaoFinanceira = await _instituicaoFinanceiraRepository.ExcluirInstituicaoFinanceira(idInstituicaoFinanceira);

            if (instituicaoFinanceira == null)
            {
                response.AddError("404", "Nenhum InstituicaoFinanceira encontrado");
                return response;
            }

            return response;
        }

        public async Task<InstituicaoFinanceiraResponse> ObterInstituicaoFinanceiraPorUf(string uf)
        {
            uf.Guard("The parameter can't be null for this operation", nameof(uf));

            var response = new InstituicaoFinanceiraResponse();

            var InstituicaoFinanceira = await _instituicaoFinanceiraRepository.ObterInstituicaoFinanceiraPorUf(uf);

            if (InstituicaoFinanceira == null)
            {
                response.AddError("404", "Nenhum InstituicaoFinanceira encontrada");
                return response;
            }

            response = InstituicaoFinanceira.ToResponse<InstituicaoFinanceiraResponse>();

            return response;
        }

        public async Task<InstituicaoFinanceiraResponse> ObterInstituicaoFinanceiraPorId(int id)
        {
            id.Guard("The parameter can't be null for this operation", nameof(id));

            var response = new InstituicaoFinanceiraResponse();

            var InstituicaoFinanceira = await _instituicaoFinanceiraRepository.ObterInstituicaoFinanceiraPorId(id);

            if (InstituicaoFinanceira == null)
            {
                response.AddError("404", "Nenhum InstituicaoFinanceira encontrada");
                return response;
            }

            response = InstituicaoFinanceira.ToResponse<InstituicaoFinanceiraResponse>();

            return response;
        }

        public async Task<ObterInstituicaoFinanceirasResponse> ObterInstituicoesFinanceiras(ObterInstituicoesFinanceirasRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new ObterInstituicaoFinanceirasResponse();

            var instituicaoFinanceira = await _instituicaoFinanceiraRepository.ObterInstituicoesFinanceiras(request);

            if (instituicaoFinanceira == null)
            {
                response.AddError("404", "Nenhum InstituicaoFinanceira encontrada");
                return response;
            }

            foreach (var item in instituicaoFinanceira.Items)
                response.InstituicoesFinanceiras.Add(item.ToResponse<InstituicaoFinanceiraResponse>());

            response.PageIndex = request.PageIndex;
            response.TotalItems = instituicaoFinanceira.TotalCount;

            return response;
        }
    }
}
