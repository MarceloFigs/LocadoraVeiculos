using LocadoraVeiculos.Models;
using System.Collections;
using System.Collections.Generic;

namespace LocadoraVeiculos.Repository
{
    public interface IClienteRepository : ICommand<Cliente>, IQuery<Cliente>
    {
        //IEnumerable<Cliente> BuscarTodos();
    }
}
