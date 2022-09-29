using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Repository.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System;
using FluentValidation;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IValidator<Categoria> _validator;
        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaRepository categoriaRepository,
            IValidator<Categoria> validator)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
            _validator = validator;
        }
        [HttpGet]
        public IActionResult BuscarCategoria([FromQuery] string id)
        {
            try
            {
                _logger.LogInformation("Buscando categoria");
                var categoria = _categoriaRepository.BuscarPorId(id);

                if (categoria == null)
                    return BadRequest("categoria não encontrada");

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CadastrarCarro([FromBody] Categoria categoria)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(categoria);

                if (!resultadoValidacao.IsValid)
                return BadRequest(resultadoValidacao.Errors);

                _categoriaRepository.Incluir(categoria);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCliente(string chassi)
        {
            try
            {
                var categoria = _categoriaRepository.BuscarPorId(chassi);

                if (categoria == null)
                    return BadRequest("Categoria não encontrado");

                _categoriaRepository.Excluir(categoria);
                return Ok("Categoria excluido com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCliente([FromBody] Categoria categoria)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(categoria);

                if (!resultadoValidacao.IsValid)
                return BadRequest(resultadoValidacao.Errors);

                _categoriaRepository.Atualizar(categoria);
                return Ok("Categoria atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao atualizar categoria");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
