using Regulatorio.Core.Validators.Dominios;
using Regulatorio.Domain.Entities.Dominios;
using Regulatorio.Domain.Enum.Dominios;
using Regulatorio.Domain.Repositories.Dominios;
using Regulatorio.Domain.Request.Dominios;
using Regulatorio.SharedKernel;
using Regulatorio.SharedKernel.Services;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.Core.Validators;
using Regulatorio.Domain.Services.Dominios;
using Regulatorio.Domain.Response.Dominios;

namespace Regulatorio.ApplicationService.Services.Dominios
{
    public class DominioAppService : BaseService, IDominioService
    {
        private readonly IDominioRepository _dominioRepository;

        public DominioAppService(IDominioRepository dominioRepository)
        {
            _dominioRepository = dominioRepository;
        }

        public async Task<DominioResponse> ObterDominio(TipoDominioEnum tipoDominio)
        {
            tipoDominio.Guard("The parameter can't be null for this operation", nameof(tipoDominio));

            var response = new DominioResponse();

            var dominios = await _dominioRepository.ObterPorTipoDominio((int)tipoDominio);

            if (dominios == null)
            {
                response.Errors.Add(new Error("404", "Nenhum dominio encontrado com o tipo informado"));
                return response;
            }

            response.TipoDominio = tipoDominio.ToString().ToUpper();

            foreach (var dominio in dominios)
            {
                response.ValorDominio.Add(new ValorDominio
                {
                    Id = dominio.Id,
                    PalavraChave = dominio.PalavraChave,
                    Valor = dominio.Valor
                });
            }

            return response;
        }

        public async Task<CriarTipoDominioResponse> CriarTipoDominio(CriarTipoDominioRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarTipoDominioResponse();
            var validator = new CriarTipoDominioValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;

            var tipoDominio = request.ToEntity<TipoDominio>();

            await _dominioRepository.SalvarTipoDominio(tipoDominio);

            return response;
        }

        public async Task<CriarDominioResponse> CriarDominio(CriarDominioRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarDominioResponse();
            var validator = new CriarDominioValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;

            var dominio = request.ToEntity<Dominio>();

            await _dominioRepository.Salvar(dominio);

            return response;
        }

        public async Task<ObterTipoDominioResponse> ObterTipoDominio(Guid usuarioGuid)
        {
            usuarioGuid.Guard("The parameter can't be null for this operation", nameof(usuarioGuid));

            var response = new ObterTipoDominioResponse();

            var tipoDominios = await _dominioRepository.ObterTipoDominio();

            if (tipoDominios == null)
            {
                response.AddError("404", "Nenhum tipo dominio encontrato");
                return response;
            }

            foreach (var tipoDominio in tipoDominios)
                response.TipoDominios.Add(tipoDominio.ToResponse<TipoDominioResponse>());

            return response;
        }

        public async Task<DominioResponse> ObterDominioPorTipo(Guid usuarioGuid, string tipoDominio)
        {
            usuarioGuid.Guard("The parameter can't be null for this operation", nameof(usuarioGuid));

            var response = new DominioResponse();

            if (string.IsNullOrEmpty(tipoDominio))
            {
                response.AddError("405", "O tipo de dominio é obrigatório");
                return response;
            }

            var dominios = await _dominioRepository.ObterPorTipoDominio(tipoDominio.ToUpper());

            if (dominios == null)
            {
                response.Errors.Add(new Error("404", "Nenhum dominio encontrado com o tipo informado"));
                return response;
            }

            response.TipoDominio = tipoDominio.ToUpper();

            foreach (var dominio in dominios)
            {
                response.ValorDominio.Add(new ValorDominio
                {
                    Id = dominio.Id,
                    PalavraChave = dominio.PalavraChave,
                    Valor = dominio.Valor
                });
            }

            return response;
        }

        public async Task<DominioResponse> ObterDominioPorId(int dominioId)
        {
            var dominio = await _dominioRepository.ObterDominioPorId(dominioId);

            if (dominio is null)
                return null;

            var tipoDominio = await _dominioRepository.ObterTipoDominio();

            var response = new DominioResponse();

            response.TipoDominio = tipoDominio.Where(td => td.Id == (int)dominio.TipoDominio).Select(td => td.Nome).ToString();

            response.ValorDominio.Add(new ValorDominio { Id = dominioId, PalavraChave = dominio.PalavraChave, Valor = dominio.Valor });

            return response;
        }
    }
}
