using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class CarroRepository : ICarroRepository
    {
        private readonly LocadoraVeiculosContext _context;
        public CarroRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public bool Incluir(Carro obj)
        {
            _context.Carro.Add(obj);
            return _context.SaveChanges() > 0;
        }
        public bool Excluir(Carro obj)
        {
            _context.Carro.Remove(obj);
            return _context.SaveChanges() > 0;
        }
        public bool Atualizar(Carro obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var resultado = _context.SaveChanges() > 0;
            _context.Entry(obj).State = EntityState.Detached;
            return resultado;
        }
        public async Task<Carro> BuscarPorIdAsync(string chassi)
        {
            var query = await _context.Carro                
                .Include(c => c.Categoria)
                .Where(c => c.Chassi == chassi)
                .FirstOrDefaultAsync();

            return query;
        }

        public async Task<IEnumerable<Carro>> BuscarTodosAsync()
        {
            return await _context.Carro.Include(c => c.Categoria).ToListAsync();
        }
    }

}
