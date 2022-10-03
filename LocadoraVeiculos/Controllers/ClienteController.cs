using FluentValidation;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Services;
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
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<Cliente> _validator;
        private readonly ICEPService _iCepService;

        public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository,
            IValidator<Cliente> validator, ICEPService cepService)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
            _validator = validator;
            _iCepService = cepService;
        }

        [HttpGet]
        public IActionResult BuscarCliente([FromQuery] string cpf)
        {
            try
            {
                _logger.LogInformation("Buscando cliente");
                var cliente = _clienteRepository.BuscarPorId(cpf);                              

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");                

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

                _clienteRepository.Incluir(cliente);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{cpf}")]
        public IActionResult ExcluirCliente(string cpf)
        {
            try
            {
                var cliente = _clienteRepository.BuscarPorId(cpf);

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");

                _clienteRepository.Excluir(cliente);
                return Ok("Cliente excluido com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao excluir cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{cpf}")]
        public IActionResult AtualizarCliente([FromBody] Cliente cliente)
        {
            try
            {
                var resultadoValidacao = _validator.Validate(cliente);

                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                _clienteRepository.Atualizar(cliente);
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
