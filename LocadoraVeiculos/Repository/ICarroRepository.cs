using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Repository
{
    public interface ICarroRepository : ICommand<Carro>, IQuery<Carro>
    {
    }
}
