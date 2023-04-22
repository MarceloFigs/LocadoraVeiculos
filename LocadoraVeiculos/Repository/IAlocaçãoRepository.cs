using LocadoraVeiculos.Models;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Repository
{
    public interface IAlocaçãoRepository : IQuery<Alocação>, ICommand<Alocação>
    {
        Task<Alocação> BuscarAlocaçãoPorCPFeChassi(string cpf, string chassi);
    }
}
