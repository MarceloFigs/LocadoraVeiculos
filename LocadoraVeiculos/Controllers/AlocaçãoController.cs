using AutoMapper;
using FluentValidation;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlocaçãoController : ControllerBase
    {
        private readonly ILogger<AlocaçãoController> _logger;
        private readonly IAlocaçãoRepository _alocaçãoRepository;
        private readonly IValidator<Alocação> _validator;
        private readonly IMapper _mapper;

        public AlocaçãoController(ILogger<AlocaçãoController> logger, IAlocaçãoRepository alocaçãoRepository,
            IValidator<Alocação> validator, IMapper mapper)
        {
            _logger = logger;
            _alocaçãoRepository = alocaçãoRepository;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult BuscarTodasAlocações()
        {
            try
            {
                _logger.LogInformation("Buscando alocação");
                var alocação = _alocaçãoRepository.BuscarTodos();

                if (alocação == null)
                    return BadRequest("alocação não encontrada");

                return Ok(_mapper.Map<IEnumerable<AlocaçãoReadDto>>(alocação));
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar alocação");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{cpf}/{chassi}", Name = "BuscarAlocaçãoPorId")]
        public IActionResult BuscarAlocaçãoPorId(string cpf, string chassi)
        {
            try
            {
                _logger.LogInformation("Buscando alocação");
                var alocação = _alocaçãoRepository.BuscarAlocação(cpf, chassi);

                if (alocação == null)
                    return BadRequest("alocação não encontrada");

                return Ok(_mapper.Map<AlocaçãoReadDto>(alocação));
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
