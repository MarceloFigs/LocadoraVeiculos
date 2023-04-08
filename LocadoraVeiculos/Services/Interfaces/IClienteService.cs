using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services.Interfaces
{
    public interface IClienteService
    {
        Task<bool> CadastrarCliente(Cliente cliente);
        Task<bool> ExcluirCliente(string cpf);
        Task<bool> AtualizarCliente(Cliente cliente);
        Task<ClienteReadDto> BuscarClienteCPFAsync(string cpf);

        Task<IEnumerable<ClienteReadDto>> BuscarClientesAsync();
    }
}
