using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class CarroRepository : ICarroRepository
    {
        private readonly LocadoraVeiculosContext _context;
        public CarroRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public void Incluir(Carro obj)
        {
            _context.Carro.Add(obj);
            _context.SaveChanges();
        }
        public void Excluir(Carro obj)
        {
            _context.Carro.Remove(obj);
            _context.SaveChanges();
        }
        public void Atualizar(Carro obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(obj).State = EntityState.Detached;
        }
        public Carro BuscarPorId(string chassi)
        {
            var query = _context.Carro                
                .Include(c => c.Categoria)
                .Where(c => c.Chassi == chassi)
                .FirstOrDefault();

            return query;
        }

        public IEnumerable<Carro> BuscarTodos()
        {
            var query = _context.Carro.Include(c => c.Categoria).ToList();

            return query;
        }
    }

}
