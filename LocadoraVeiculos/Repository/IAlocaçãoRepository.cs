using LocadoraVeiculos.Models;
using System.Collections.Generic;

namespace LocadoraVeiculos.Repository
{
    public interface IAlocaçãoRepository : IQuery<Alocação>, ICommand<Alocação>
    {
        IEnumerable<Alocação> BuscarTodos();
        Alocação BuscarAlocação(string cpf, string chassi);
    }
}
