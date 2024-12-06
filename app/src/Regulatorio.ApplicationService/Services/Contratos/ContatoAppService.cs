using Regulatorio.Core.Validators;
using Regulatorio.Core.Validators.Contatos;
using Regulatorio.Domain.Entities.Registros;
using Regulatorio.Domain.Repositories.Contatos;
using Regulatorio.Domain.Request.Contatos;
using Regulatorio.Domain.Request.Registros;
using Regulatorio.Domain.Response.Contatos;
using Regulatorio.Domain.Response.Registradoras;
using Regulatorio.Domain.Response.Registros;
using Regulatorio.Domain.Services.Contatos;
using Regulatorio.SharedKernel;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.SharedKernel.Services;

namespace Regulatorio.ApplicationService.Services.Contatos
{
    public class ContatoAppService : BaseService, IContatoService
    {
        private IContatoRepository _contatoRepository;

        public ContatoAppService(IContatoRepository ContatoRepository)
        {
            _contatoRepository = ContatoRepository;
        }

        public async Task<ObterContatoResponse> ObterContatos(ObterContatoRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new ObterContatoResponse();

            var contatos = await _contatoRepository.ObterContatos(request);

            if (contatos == null)
            {
                response.AddError("404", "Nenhum registro encontrado");
                return response;
            }

            foreach (var contato in contatos.Items)
                response.Contatos.Add(contato.ToResponse<ContatoResponse>());

            response.PageIndex = request.PageIndex;
            response.TotalItems = contatos.TotalCount;

            return response;
        }

        public async Task<CriarContatoResponse> CriarContato(CriarContatoRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new CriarContatoResponse();
            var validator = new CriarContatosValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;


            var exists = await _contatoRepository.ObterContatoPorUf(request.Uf);

            if (exists != null)
            {
                response.AddError("409", "Não é possível criar um novo contato. Existe um cadastro vinculado ao estado.");
                return response;
            }

            var Contato = await _contatoRepository.CriarContato(request);

            if (Contato == null)
            {
                response.AddError("500", "Ocorreu um erro ao criar um novo Contato");
                return response;
            }

            response.Id = Contato;

            return response;
        }

        public async Task<ContatoResponse> EditarContato(int idContato, EditarContatoRequest request)
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            var response = new ContatoResponse();
            var validator = new EditarContatosValidator();

            var validacaoResult = validator.ValidateRequest(request);
            response.AddError(validacaoResult.Errors);

            if (!response.IsSuccess)
                return response;

            if (!string.IsNullOrEmpty(request.Uf))
            {
                var exists = await _contatoRepository.ObterContatoPorUf(request.Uf);

                if (exists != null && exists?.Id != idContato)
                {
                    response.AddError("409", "Não é possível editar o contato. Existe um cadastro vinculado ao estado.", "uf");
                    return response;
                }
            }

            var _notExist = await _contatoRepository.ObterContatoPorId(idContato);

            if (_notExist == null )
            {
                response.AddError("409", "Não é possível editar o contato.");
                return response;
            }

            var registro = await _contatoRepository.EditarContato(idContato, request);


            if (registro == null)
            {
                response.AddError("500", "Ocorreu um erro ao editar o contato");
                return response;
            }

            response.Id = idContato;

            response = registro.ToResponse<ContatoResponse>();

            return response;
        }



        public async Task<ExcluirContatoResponse> ExcluirContato(int idContato)
        {
            idContato.Guard("The parameter can't be null for this operation", nameof(idContato));

            var response = new ExcluirContatoResponse();

            var registradoras = await _contatoRepository.ExcluirContato(idContato);

            if (registradoras == 0)
            {
                response.AddError("404", "Nenhuma Registradora encontrado");
                return response;
            }

            return response;
        }

    }
}
