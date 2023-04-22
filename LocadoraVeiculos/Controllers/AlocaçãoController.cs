using FluentValidation;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlocaçãoController : ControllerBase
    {
        private readonly ILogger<AlocaçãoController> _logger;
        private readonly IAlocaçãoService _alocaçãoService;
        private readonly IValidator<Alocação> _validator;

        public AlocaçãoController(ILogger<AlocaçãoController> logger, IAlocaçãoService alocaçãoService,
            IValidator<Alocação> validator)
        {
            _logger = logger;
            _alocaçãoService = alocaçãoService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodasAlocações()
        {
            try
            {
                _logger.LogInformation("Buscando alocação");
                var alocação = await _alocaçãoService.BuscarTodasAlocaçõesAsync();

                if (alocação is null)
                    return NotFound("alocação não encontrada");

                return Ok(alocação);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{cpf}/{chassi}", Name = "BuscarAlocaçãoPorCPFeChassi")]
        public async Task<IActionResult> BuscarAlocaçãoPorCPFeChassi(string cpf, string chassi)
        {
            try
            {
                _logger.LogInformation("Buscando alocação");
                var alocação = await _alocaçãoService.BuscarAlocaçãoPorCPFeChassiAsync(cpf, chassi);

                if (alocação is null)
                    return NotFound("alocação não encontrada");

                return Ok(alocação);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAlocação([FromBody] Alocação alocação)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(alocação);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                var resultado = await _alocaçãoService.CadastrarAlocação(alocação);
                if (resultado is true)
                    return Ok("Alocação cadastrada com sucesso!");

                return BadRequest("Erro ao tentar cadastrar alocação");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{cpf}/{chassi}")]
        public async Task<IActionResult> ExcluirAlocação(string cpf, string chassi)
        {
            try
            {
                var resultado = await _alocaçãoService.ExcluirAlocação(cpf, chassi);
                if (resultado is true)
                    return Ok("Alocação excluida com sucesso!");

                return BadRequest("Erro ao tentar excluir alocação");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{cpf}/{chassi}")]
        public async Task<IActionResult> AtualizarAlocação([FromBody] Alocação alocação)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(alocação);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                var resultado = await _alocaçãoService.AtualizarAlocação(alocação);
                if (resultado is true)
                    return Ok("Alocação atualizada com sucesso!");

                return BadRequest("Erro ao atualizar alocação");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao atualizar alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
