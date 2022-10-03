using System.Collections;
using System.Collections.Generic;

namespace LocadoraVeiculos.Repository
{
    public interface IQuery<T>
    {
        T BuscarPorId(string id);
    }
}
