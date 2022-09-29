using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Repository
{
    public interface ICategoriaRepository : ICommand<Categoria>, IQuery<Categoria>
    {
    }
}
