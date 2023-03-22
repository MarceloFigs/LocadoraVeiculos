using AutoMapper;
using FluentValidation;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Services;
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
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<Cliente> _validator;
        private readonly ICEPService _iCepService;
        private readonly IMapper _mapper;

        public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository,
            IValidator<Cliente> validator, ICEPService cepService, IMapper mapper)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
            _validator = validator;
            _iCepService = cepService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult BuscarClientes()
        {
            try
            {
                _logger.LogInformation("Buscando clientes");
                var cliente = _clienteRepository.BuscarTodos();

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");

                return Ok(_mapper.Map<IEnumerable<ClienteReadDto>>(cliente));
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar cliente");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{cpf}", Name = "BuscarClienteCPF")]
        public IActionResult BuscarClienteCPF(string cpf)
        {
            try
            {
                _logger.LogInformation("Buscando cliente");
                var cliente = _clienteRepository.BuscarPorId(cpf);                              

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");

                return Ok(_mapper.Map<ClienteReadDto>(cliente));
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
        public async Task<IActionResult> AtualizarCliente([FromBody] Cliente cliente)
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
