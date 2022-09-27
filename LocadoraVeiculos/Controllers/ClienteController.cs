using FluentValidation;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<Cliente> _validator;        

        public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository,
            IValidator<Cliente> validator)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
            _validator = validator;            
        }

        [HttpGet]
        public IActionResult BuscarCliente([FromQuery] string cpf)
        {
            try
            {
                _logger.LogInformation("Buscando cliente");
                var cliente = _clienteRepository.GetClienteByCpf(cpf);                              

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar cliente");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CadastrarCliente([FromBody] Cliente cliente)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(cliente);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                _clienteRepository.CadastrarCliente(cliente);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar cliente");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{cpf}")]
        public IActionResult ExcluirCliente(string cpf)
        {
            try
            {
                var cliente = _clienteRepository.GetClienteByCpf(cpf);

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");

                _clienteRepository.ExcluirCliente(cliente);
                return Ok("Cliente excluido com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir cliente");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{cpf}")]
        public IActionResult AtualizarCliente([FromBody] Cliente cliente, string cpf)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(cliente);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                _clienteRepository.AtualizarCliente(cliente);
                return Ok("Cliente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao atualizar cliente");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
