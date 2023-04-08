using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly LocadoraVeiculosContext _context;
        public CategoriaRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public bool Atualizar(Categoria obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var resultado = _context.SaveChanges() > 0;
            _context.Entry(obj).State = EntityState.Detached;
            return resultado;
        }

        public async Task<Categoria> BuscarPorIdAsync(string id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(c => c.CodCategoria == int.Parse(id));
        }

        public async Task<IEnumerable<Categoria>> BuscarTodosAsync()
        {
            return await _context.Categoria.ToListAsync();
        }

        public bool Excluir(Categoria obj)
        {
            _context.Categoria.Remove(obj);
            return _context.SaveChanges() > 0;
        }

        public bool Incluir(Categoria obj)
        {
            _context.Categoria.Add(obj);
            return _context.SaveChanges() > 0;
        }
    }
}
