using LocadoraVeiculos.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services
{
    public class CEPService : ICEPService
    {
        private IHttpClientFactory _httpClientFactory;

        public CEPService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;            
        }

        public async Task<CEP> BuscarCEP(int cep)
        {

            var client = _httpClientFactory.CreateClient("CepService");
            var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var content = await response.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<CEP>(content);

            return endereco;
        }
    }
}
