using System.Collections;
using System.Collections.Generic;

namespace LocadoraVeiculos.Repository
{
    public interface IQuery<T>
    {
        //IEnumerable<T> BuscarTodos();
        T BuscarPorId(string id);
    }
}
