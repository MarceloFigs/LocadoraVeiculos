using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Repository
{
    public interface IAlocaçãoRepository : IQuery<Alocação>, ICommand<Alocação>
    {
        Alocação BuscarAlocação(string cpf, string chassi);
    }
}
