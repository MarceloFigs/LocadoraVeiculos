using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Repository
{
    public interface ICommand<T>
    {
        bool Incluir(T obj);
        bool Excluir(T obj);
        bool Atualizar(T obj);

        Task<IEnumerable<T>> BuscarTodosAsync();
    }
}
