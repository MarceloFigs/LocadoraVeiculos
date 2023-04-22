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
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaService _categoriaService;
        private readonly IValidator<Categoria> _validator;
        
        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaService categoriaService,
            IValidator<Categoria> validator)
        {
            _logger = logger;
            _categoriaService = categoriaService;            
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarCategoria()
        {
            try
            {
                _logger.LogInformation("Buscando categoria");
                var categoria = await _categoriaService.BuscarCategoriasAsync();

                if (categoria is null)
                    return NotFound("categoria não encontrada");

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "BuscarCategoriaPorID")]
        public async Task<IActionResult> BuscarCategoriaPorId(int id)
        {
            try
            {
                _logger.LogInformation("Buscando categoria");
                var categoria = await _categoriaService.BuscarCategoriaPorIdAsync(id);

                if (categoria is null)
                    return NotFound("categoria não encontrada");

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(categoria);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                var resultado = await _categoriaService.CadastrarCategoria(categoria);
                if (resultado is true)
                    return Ok("Categoria cadastrada com sucesso!");

                return BadRequest("Erro ao tentar cadastrar categoria");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCategoria(int id)
        {
            try
            {
                var resultado = await _categoriaService.ExcluirCategoria(id);
                if (resultado is true) 
                    return Ok("Categoria excluida com sucesso!");
                
                return BadRequest("Categoria não encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(categoria);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                var resultado = _categoriaService.AtualizarCategoria(categoria);
                if (resultado is true)
                    return Ok("Categoria atualizado com sucesso!");

                return BadRequest("Erro ao atualizar categoria");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao atualizar categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
