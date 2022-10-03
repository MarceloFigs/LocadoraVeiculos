using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }
        public Carro BuscarPorId(string chassi)
        {
            var query = _context.Carro
                .Where(c => c.Chassi == chassi)
                .Include(c => c.Categoria).FirstOrDefault();

            return query;
        }
    }

}
