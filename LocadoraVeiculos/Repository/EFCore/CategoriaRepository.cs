using LocadoraVeiculos.Models;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly LocadoraVeiculosContext _context;
        public CategoriaRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public void Atualizar(Categoria obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        public Categoria BuscarPorId(string id)
        {
            return _context.Categoria.FirstOrDefault(c => c.CodCategoria == int.Parse(id));
        }

        public IEnumerable<Categoria> BuscarTodos()
        {
            return _context.Categoria.ToList();
        }

        public void Excluir(Categoria obj)
        {
            _context.Categoria.Remove(obj);
            _context.SaveChanges();
        }

        public void Incluir(Categoria obj)
        {
            _context.Categoria.Add(obj);
            _context.SaveChanges();
        }
    }
}
