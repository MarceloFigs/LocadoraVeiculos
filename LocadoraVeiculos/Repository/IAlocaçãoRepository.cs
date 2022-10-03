using LocadoraVeiculos.Models;
using System.Collections.Generic;

namespace LocadoraVeiculos.Repository
{
    public interface IAlocaçãoRepository : IQuery<Alocação>, ICommand<Alocação>
    {
        Alocação BuscarAlocação(string cpf, string chassi);
    }
}
