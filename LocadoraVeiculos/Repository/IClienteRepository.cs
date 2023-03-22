using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Repository
{
    public interface IClienteRepository : ICommand<Cliente>, IQuery<Cliente>
    {
    }
}
