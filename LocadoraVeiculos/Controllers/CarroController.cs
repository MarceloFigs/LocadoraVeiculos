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
using System.Threading.Tasks;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarroController: ControllerBase
    {
        private readonly ILogger<CarroController> _logger;
        private readonly ICarroRepository _carroRepository;
        private readonly IValidator<Carro> _validator;
        private readonly IMapper _mapper;
        public CarroController(ILogger<CarroController> logger, ICarroRepository carroRepository,
            IValidator<Carro> validator, IMapper mapper)
        {
            _logger = logger;
            _carroRepository = carroRepository;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult BuscarCarros()
        {
            try
            {
                _logger.LogInformation("Buscando carro");
                var carro = _carroRepository.BuscarTodosAsync();

                if (carro == null)
                    return BadRequest("Carro não encontrado");

                return Ok(_mapper.Map<IEnumerable<CarroReadDto>>(carro));
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{chassi}", Name = "BuscarCarroPorChassi")]
        public IActionResult BuscarCarroPorChassi(string chassi)
        {
            try
            {
                _logger.LogInformation("Buscando carro");
                var carro = _carroRepository.BuscarPorIdAsync(chassi);

                if (carro == null)
                    return BadRequest("Carro não encontrado");

                return Ok(_mapper.Map<CarroReadDto>(carro));
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CadastrarCarro([FromBody] Carro carro)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(carro);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                _carroRepository.Incluir(carro);
                return Ok(carro);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{chassi}")]
        public async Task<IActionResult> ExcluirCliente(string chassi)
        {
            try
            {
                var carro = await _carroRepository.BuscarPorIdAsync(chassi);

                if (carro == null)
                    return BadRequest("Carro não encontrado");

                _carroRepository.Excluir(carro);
                return Ok("Carro excluido com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{chassi}")]
        public IActionResult AtualizarCliente([FromBody] Carro carro)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(carro);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                _carroRepository.Atualizar(carro);
                return Ok("Carro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao atualizar carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
