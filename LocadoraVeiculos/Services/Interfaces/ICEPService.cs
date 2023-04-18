using LocadoraVeiculos.Models;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services.Interfaces
{
    public interface ICEPService
    {
        Task<CEP> BuscarCEP(int cep);
        Cliente AtribuirCep(Cliente cliente, CEP cep);
    }
}
