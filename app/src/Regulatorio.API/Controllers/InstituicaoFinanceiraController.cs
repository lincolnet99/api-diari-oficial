using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Request.InstituicaoFinanceira;
using Regulatorio.Domain.Services.InstituicaoFinanceira;

namespace Regulatorio.API.Controllers
{
    [Route("api/instituicaofinanceira")]
    [ApiController]
    public class InstituicaoFinanceiraController : BaseController
    {
        private readonly IInstituicaoFinanceiraService _instituicaoFinanceiraervice;

        public InstituicaoFinanceiraController(IInstituicaoFinanceiraService InstituicaoFinanceiraervice)
        {
            _instituicaoFinanceiraervice = InstituicaoFinanceiraervice;
        }

        [HttpGet]
        public async Task<IActionResult> ObterInstituicaoFinanceira([FromQuery] ObterInstituicoesFinanceirasRequest request)
        {
            var response = await _instituicaoFinanceiraervice.ObterInstituicoesFinanceiras(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{uf}")]
        public async Task<IActionResult> ObterInstituicaoFinanceiraPorUf(string uf)
        {
            var response = await _instituicaoFinanceiraervice.ObterInstituicaoFinanceiraPorUf(uf);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterInstituicaoFinanceiraPorId(int id)
        {
            var response = await _instituicaoFinanceiraervice.ObterInstituicaoFinanceiraPorId(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CriarInstituicaoFinanceira([FromBody] CriarInstituicaoFinanceiraRequest request)
        {
            var response = await _instituicaoFinanceiraervice.CriarInstituicaoFinanceira(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> EditarInstituicaoFinanceira([FromBody] EditarInstituicaoFinanceiraRequest request)
        {
            var response = await _instituicaoFinanceiraervice.EditarInstituicaoFinanceira(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirInstituicaoFinanceira(int id)
        {
            var response = await _instituicaoFinanceiraervice.ExcluirInstituicaoFinanceira(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }
    }
}
