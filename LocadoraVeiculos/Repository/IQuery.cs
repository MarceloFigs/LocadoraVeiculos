using System.Threading.Tasks;

namespace LocadoraVeiculos.Repository
{
    public interface IQuery<T>
    {
        Task<T> BuscarPorIdAsync(string id);
    }
}
