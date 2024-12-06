using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Request.Registradoras;
using Regulatorio.Domain.Services.Registradoras;

namespace Regulatorio.API.Controllers
{
    [Route("api/Registradoras")]
    [ApiController]
    public class RegistradorasController : BaseController
    {
        private readonly IRegistradorasService _registradoraservice;

        public RegistradorasController(IRegistradorasService Registradoraservice)
        {
            _registradoraservice = Registradoraservice;
        }

        [HttpGet]
        public async Task<IActionResult> ObterRegistradoras([FromQuery] ObterRegistradorasRequest request)
        {
            var response = await _registradoraservice.ObterRegistradoras(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("uf/{uf}")]
        public async Task<IActionResult> ObterRegistradoraPorUf(string uf)
        {
            var response = await _registradoraservice.ObterRegistradoraPorUf(uf);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }
        [HttpGet("empresa/{empresa}")]
        public async Task<IActionResult> ObterRegistradoraPorEmpresa(string empresa)
        {
            var response = await _registradoraservice.ObterRegistradoraPorEmpresa(empresa);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterRegistradoraPorId(int id)
        {
            var response = await _registradoraservice.ObterRegistradoraPorId(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CriarRegistradora([FromForm] CriarRegistradoraRequest request)
        {
            var response = await _registradoraservice.CriarRegistradora(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarRegistradora(int id, [FromForm] EditarRegistradoraRequest request)
        {
            var response = await _registradoraservice.EditarRegistradora(id, request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirRegistradora(int id)
        {
            var response = await _registradoraservice.ExcluirRegistradora(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadeRegistradora(int id)
        {
            var response = await _registradoraservice.ObterRegistradoraParaDownload(id);

            if (response.IsSuccess)
            {
                return File(response.Arquivo.ToArray(), "application/octet-stream", response.NomeArquivo);
            }

            return Error(401, response.Errors);
        }
    }
}
