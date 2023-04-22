using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public bool AtualizarCategoria(Categoria categoria)
        {
            return _categoriaRepository.Atualizar(categoria);
        }

        public async Task<Categoria> BuscarCategoriaPorIdAsync(int id)
        {
            return await _categoriaRepository.BuscarPorIdAsync(id.ToString());
        }

        public async Task<IEnumerable<Categoria>> BuscarCategoriasAsync()
        {
            return await _categoriaRepository.BuscarTodosAsync();
        }

        public async Task<bool> CadastrarCategoria(Categoria categoria)
        {
            var categoriaExiste = await _categoriaRepository.BuscarPorIdAsync(categoria.CodCategoria.ToString());
            if (categoriaExiste != null) return false;

            return _categoriaRepository.Incluir(categoria);
        }

        public async Task<bool> ExcluirCategoria(int id)
        {
            var categoria = await _categoriaRepository.BuscarPorIdAsync(id.ToString());
            if (categoria is null) return false;

            return _categoriaRepository.Excluir(categoria);
        }
    }
}
