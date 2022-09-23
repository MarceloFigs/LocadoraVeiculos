using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Repository
{
    public interface IClienteRepository
    {
        void CadastrarCliente(Cliente cliente);
        Cliente GetClienteByCpf(string cpf);
        void AtualizarCliente(Cliente cliente);
        void ExcluirCliente(Cliente cliente);
    }
}
