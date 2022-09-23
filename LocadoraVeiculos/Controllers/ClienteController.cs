using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public IActionResult BuscarCliente([FromQuery] string cpf)
        {
            var cliente = _clienteRepository.GetClienteByCpf(cpf);
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult CadastrarCliente([FromBody] Cliente cliente)
        {
            _clienteRepository.CadastrarCliente(cliente);
            return Ok(cliente);
        }

        [HttpDelete("{cpf}")]
        public IActionResult ExcluirCliente(string cpf)
        {
            var cliente = _clienteRepository.GetClienteByCpf(cpf);
            _clienteRepository.ExcluirCliente(cliente);
            return Ok("Cliente excluido com sucesso!");
        }

        [HttpPut("{cpf}")]
        public IActionResult AtualizarCliente([FromBody] Cliente model, string cpf)
        {
            var cliente = _clienteRepository.GetClienteByCpf(cpf);

            if (!string.IsNullOrEmpty(model.Nome) && !string.IsNullOrWhiteSpace(model.Nome))
            {
                cliente.Nome = model.Nome;
            }
            cliente.RG = model.RG;
            cliente.DtNascimento = model.DtNascimento;
            cliente.Cnh = model.Cnh;
            cliente.Endereço = model.Endereço;

            _clienteRepository.AtualizarCliente(cliente);

            return Ok(new { msg = "Cadastro cliente atualizado com sucesso!" });
        }
    }
}
