using LocadoraVeiculos.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services
{
    public class CEPService : ICEPService
    {
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public CEPService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<CEP> BuscarCEP(int cep)
        {
            var client = _httpClientFactory.CreateClient("CepService");
            var url = $"{_configuration["CepServiceUrl:BaseUrl"]}{cep}/json";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<CEP>(content);

            return endereco;
        }
        public Cliente AtribuirCep(Cliente cliente, CEP cep)
        { 
            cliente.UF = cep.UF;
            cliente.Cidade = cep.Cidade;
            cliente.Logradouro = cep.Logradouro;

            return cliente;
        }
    }
}
