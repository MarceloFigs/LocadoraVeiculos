using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using LocadoraVeiculos.Controllers;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocadoraVeiculos.UnitTests.Controllers
{
    public class ClienteControllerUnitTests
    {
        private ClienteController _clienteController;
        private ILogger<ClienteController> _logger;
        private IClienteService _clienteService;
        private IValidator<Cliente> _validator;

        public ClienteControllerUnitTests()
        {
            //Dependencias
            _clienteService = A.Fake<IClienteService>();
            _validator = A.Fake<IValidator<Cliente>>();

            //SUT
            _clienteController = new ClienteController(_logger, _clienteService, _validator);
        }

        [Fact]
        public void ClienteController_BuscaClientes_RetornaComSucesso()
        {
            //Arrange
            var clientes = A.Fake<IEnumerable<ClienteReadDto>>();
            A.CallTo(() => _clienteService.BuscarClientesAsync()).Returns(clientes);

            //Act
            var listaClientes = _clienteController.BuscarClientes();

            //Assert
            listaClientes.Should().BeOfType<Task<IActionResult>>();
            listaClientes.Should().NotBeNull();
        }

        [Fact]
        public void ClienteController_BuscaClienteCPF_RetornaComSucesso()
        {
            //Arrange
            var cpf = "444";
            var clienteFake = A.Fake<ClienteReadDto>();
            A.CallTo(() => _clienteService.BuscarClienteCPFAsync(cpf)).Returns(clienteFake);

            //Act
            var clientePorCpf = _clienteController.BuscarClienteCPF(cpf);

            //Assert
            clientePorCpf.Should().BeOfType<Task<IActionResult>>();
            clientePorCpf.Should().NotBeNull();
        }

        [Fact]
        public void ClienteController_CadastrarCliente_RetornaComSucesso()
        {
            //Arrange
            var clienteFake = A.Fake<Cliente>();
            A.CallTo(() => _validator.Validate(clienteFake));
            A.CallTo(() => _clienteService.CadastrarCliente(clienteFake));

            //Act            
            var clienteCadastrado = _clienteController.CadastrarCliente(clienteFake);

            //Assert
            clienteCadastrado.Should().BeOfType<Task<IActionResult>>();
            clienteCadastrado.Should().NotBeNull();
        }

        [Fact]
        public void ClienteController_ExcluirCliente_RetornaComSucesso()
        {
            //Arrange
            var cpf = "";
            A.CallTo(() => _clienteService.ExcluirCliente(cpf));

            //Act            
            var resultado = _clienteController.ExcluirCliente(cpf);

            //Assert
            resultado.Should().BeOfType<Task<IActionResult>>();
            resultado.Should().NotBeNull();
        }

        [Fact]
        public void ClienteController_AtualizarCliente_RetornaComSucesso()
        {
            //Arrange
            var cliente = A.Fake<Cliente>();
            A.CallTo(() => _validator.Validate(cliente));
            A.CallTo(() => _clienteService.AtualizarCliente(cliente));

            //Act            
            var clienteAtualizado = _clienteController.AtualizarCliente(cliente);

            //Assert
            clienteAtualizado.Should().BeOfType<Task<IActionResult>>();
            clienteAtualizado.Should().NotBeNull();
        }
    }
}
