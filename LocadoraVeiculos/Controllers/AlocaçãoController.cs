using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Repository.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System;
using LocadoraVeiculos.Models;
using FluentValidation;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlocaçãoController : ControllerBase
    {
        private readonly ILogger<AlocaçãoController> _logger;
        private readonly IAlocaçãoRepository _alocaçãoRepository;
        private readonly IValidator<Alocação> _validator;

        public AlocaçãoController(ILogger<AlocaçãoController> logger, IAlocaçãoRepository alocaçãoRepository,
            IValidator<Alocação> validator)
        {
            _logger = logger;
            _alocaçãoRepository = alocaçãoRepository;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult BuscarAlocação([FromQuery] string cpf, string chassi)
        {
            try
            {
                _logger.LogInformation("Buscando alocação");
                var alocação = _alocaçãoRepository.BuscarAlocação(cpf, chassi);

                if (alocação == null)
                    return BadRequest("alocação não encontrada");

                return Ok(alocação);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CadastrarAlocação([FromBody] Alocação alocação)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(alocação);

                if (!resultadoValidacao.IsValid)
                   return BadRequest(resultadoValidacao.Errors);

                _alocaçãoRepository.Incluir(alocação);
                return Ok(alocação);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
