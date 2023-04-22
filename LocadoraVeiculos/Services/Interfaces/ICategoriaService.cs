using LocadoraVeiculos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<bool> CadastrarCategoria(Categoria categoria);
        Task<bool> ExcluirCategoria(int id);
        bool AtualizarCategoria(Categoria categoria);
        Task<Categoria> BuscarCategoriaPorIdAsync(int id);

        Task<IEnumerable<Categoria>> BuscarCategoriasAsync();
    }
}
