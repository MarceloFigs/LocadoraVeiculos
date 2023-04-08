using FluentValidation;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Services;
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
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;
        private readonly IValidator<Cliente> _validator;
        private readonly ICEPService _iCepService;

        public ClienteController(ILogger<ClienteController> logger, IClienteService clienteService,
            IValidator<Cliente> validator, ICEPService cepService)
        {
            _logger = logger;
            _clienteService = clienteService;
            _validator = validator;
            _iCepService = cepService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarClientes()
        {
            try
            {
                _logger.LogInformation("Buscando clientes");
                var cliente = await _clienteService.BuscarClientesAsync();

                if (cliente is null)
                    return NotFound("Cliente não encontrado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{cpf}", Name = "BuscarClienteCPF")]
        public async Task<IActionResult> BuscarClienteCPF(string cpf)
        {
            try
            {
                _logger.LogInformation("Buscando cliente");
                var cliente = await _clienteService.BuscarClienteCPFAsync(cpf);

                if (cliente is null)
                    return NotFound("Cliente não encontrado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCliente([FromBody] Cliente cliente)
        {
            try
            {
                var endereco = await _iCepService.BuscarCEP(cliente.CEP);

                if (endereco.UF == null)
                    return BadRequest("cep invalido");

                cliente.UF = endereco.UF;
                cliente.Cidade = endereco.Cidade;
                cliente.Logradouro = endereco.Logradouro;

                var resultadoValidacao = _validator.Validate(cliente);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                _clienteService.IncluirCliente(cliente);
                return Ok(cliente);
                //var resultadoValidacao = _validator.Validate(cliente);

                //if (!resultadoValidacao.IsValid)
                //    return BadRequest(resultadoValidacao.Errors);

                //var resultado = _clienteService.IncluirCliente(cliente);
                //if (resultado is true) 
                //    return Ok("Cliente cadastrado com sucesso!");

                //return BadRequest("Erro ao tentar cadastrar cliente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> ExcluirCliente(string cpf)
        {
            try
            {
                var resultado = await _clienteService.ExcluirCliente(cpf);
                if (resultado is true) return Ok("Cliente excluido com sucesso!");

                return BadRequest("Erro ao tentar excluir cliente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> AtualizarCliente([FromBody] Cliente cliente)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(cliente);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                var resultado = await _clienteService.AtualizarCliente(cliente);

                if (resultado is false)
                    return BadRequest("Erro ao atualizar cliente");

                return Ok("Cliente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao atualizar cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
