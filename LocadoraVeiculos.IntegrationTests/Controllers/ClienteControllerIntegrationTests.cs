using FluentAssertions;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.IntegrationTests.Creator;
using LocadoraVeiculos.IntegrationTests.Setup;
using LocadoraVeiculos.Repository.EFCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace LocadoraVeiculos.IntegrationTests.Controllers
{
    public class ClienteControllerIntegrationTests : IntegrationTestBase
    {
        private readonly IntegrationTestFactory<Program, LocadoraVeiculosContext> _integrationTestFactory;
        private readonly ClienteCreator _clienteCreator;
        private readonly HttpClient _httpClient;

        public ClienteControllerIntegrationTests(
            IntegrationTestFactory<Program, LocadoraVeiculosContext> integrationTestFactory)
            : base(integrationTestFactory)
        {
            _integrationTestFactory = integrationTestFactory;
            var scope = integrationTestFactory.Services.CreateScope();
            _clienteCreator = scope.ServiceProvider.GetService<ClienteCreator>();
            _httpClient = _integrationTestFactory.CreateClient();
        }

        [Fact]
        public async Task ClienteController_BuscarClientes_RetornaListaClientesReadDto()
        {
            //Act
            await _clienteCreator.AddClientesAsync();

            //Arrange            
            var listaClientes = await _httpClient.GetFromJsonAsync<IEnumerable<ClienteReadDto>>("/api/Cliente");

            //Assert
            listaClientes.Should().NotBeNull();
            listaClientes.Should().BeOfType<List<ClienteReadDto>>();
            listaClientes.Count().Should().Be(6);
        }

        [Fact]
        public async Task ClienteController_BuscarClientesCPF_RetornaClienteReadDto()
        {
            //Act
            var cliente = await _clienteCreator.CreateClienteAsync();

            //Arrange            
            var listaClientes = await _httpClient.GetFromJsonAsync<ClienteReadDto>($"/api/Cliente/{cliente.Cpf}");
            var clienteFromDb = _dbContext.Cliente.SingleOrDefault(x => x.Cpf == cliente.Cpf);

            //Assert
            listaClientes.Should().NotBeNull();
            listaClientes.Should().BeOfType<ClienteReadDto>();
            clienteFromDb.Cpf.Should().Be(cliente.Cpf);
        }

        [Fact]
        public async Task ClienteController_CadastrarCliente_RetornaOK()
        {
            //Act
            var cliente = ClienteCreator.CreateCliente();

            //Arrange
            var response = await _httpClient.PostAsJsonAsync($"/api/Cliente", cliente);
            var clienteFromDb = _dbContext.Cliente.SingleOrDefault(x => x.Cpf == cliente.Cpf);

            //Assert
            response.Should().NotBeNull();
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            clienteFromDb.Cpf.Should().Be(cliente.Cpf);

            await _httpClient.DeleteAsync($"/api/Cliente/{cliente.Cpf}");
        }

        [Fact]
        public async Task ClienteController_ExcluirCliente_RetornaOK()
        {
            //Act            
            var cliente = await _clienteCreator.CreateClienteAsync();

            //Arrange
            var response = await _httpClient.DeleteAsync($"/api/Cliente/{cliente.Cpf}");
            var clienteFromDb = _dbContext.Cliente.SingleOrDefault(x => x.Cpf == cliente.Cpf);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            clienteFromDb.Should().BeNull();
        }

        [Fact]
        public async Task ClienteController_AtualizarCliente_RetornaOK()
        {
            //Act
            var cliente = await _clienteCreator.CreateClienteAsync();
            var clienteAtualizado = _clienteCreator.AtualizaCliente(cliente);

            //Arrange
            var clienteFromDb = _dbContext.Cliente.SingleOrDefault(x => x.Cpf == cliente.Cpf);
            var response = await _httpClient.PutAsJsonAsync($"/api/Cliente/{cliente.Cpf}", clienteAtualizado);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            clienteFromDb.Should().NotBeEquivalentTo(cliente);

            await _httpClient.DeleteAsync($"/api/Cliente/{cliente.Cpf}");
        }
    }
}
