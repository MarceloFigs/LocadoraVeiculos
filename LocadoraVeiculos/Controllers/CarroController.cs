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
    public class CarroController: ControllerBase
    {
        private readonly ILogger<CarroController> _logger;
        private readonly ICarroService _carroService;
        //private readonly ICarroRepository _carroRepository;
        private readonly IValidator<Carro> _validator;
        //private readonly IMapper _mapper;
        public CarroController(ILogger<CarroController> logger, ICarroService carroService,//ICarroRepository carroRepository,
            IValidator<Carro> validator/*, IMapper mapper*/)
        {
            _logger = logger;
            _carroService = carroService;
            //_carroRepository = carroRepository;
            _validator = validator;
            //_mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarCarros()
        {
            try
            {
                _logger.LogInformation("Buscando carro");
                var carro = await _carroService.BuscarCarrosAsync();

                if (carro is null)
                    return NotFound("Carro não encontrado");

                return Ok(carro);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{chassi}", Name = "BuscarCarroPorChassi")]
        public async Task<IActionResult> BuscarCarroPorChassi(string chassi)
        {
            try
            {
                _logger.LogInformation("Buscando carro");
                var carro = await _carroService.BuscarCarroChassiAsync(chassi);

                if (carro is null)
                    return NotFound("Carro não encontrado");

                return Ok(carro);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCarro([FromBody] Carro carro)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(carro);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                var resultado = await _carroService.CadastrarCarro(carro);
                if (resultado is true)
                    return Ok("Carro cadastrado com sucesso!");

                return BadRequest("Erro ao tentar cadastrar carro");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{chassi}")]
        public async Task<IActionResult> ExcluirCarro(string chassi)
        {
            try
            {
                var resultado = await _carroService.ExcluirCarro(chassi);
                if (resultado is true)
                    return Ok("Carro excluido com sucesso!");

                return BadRequest("Erro ao tentar excluir carro");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir carro");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{chassi}")]
        public async Task<IActionResult> AtualizarCarro([FromBody] Carro carro)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(carro);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                var resultado = await _carroService.AtualizarCarro(carro);
                if (resultado is false)
                    return BadRequest("Erro ao atualizar carro");
                
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
