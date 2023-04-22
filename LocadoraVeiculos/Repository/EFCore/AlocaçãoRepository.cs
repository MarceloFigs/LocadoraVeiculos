using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class AlocaçãoRepository : IAlocaçãoRepository
    {
        private readonly LocadoraVeiculosContext _context;
        public AlocaçãoRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public bool Atualizar(Alocação obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var resultado = _context.SaveChanges() > 0;
            _context.Entry(obj).State = EntityState.Detached;
            return resultado;
        }
        public async Task<Alocação> BuscarAlocaçãoPorCPFeChassi(string cpf, string chassi)
        {
            var query = await _context.Alocação
                .Include(c => c.Cliente)
                .Include(c => c.Carro)
                .Include(c => c.Carro.Categoria)
                .Where(a => a.Cpf == cpf && a.Chassi == chassi)
                .FirstOrDefaultAsync();

            return query;
        }
        public async Task<Alocação> BuscarPorIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Alocação>> BuscarTodosAsync()
        {
            var query = await _context.Alocação
                .Include(cl => cl.Cliente)
                .Include(cr => cr.Carro).Include(c => c.Carro.Categoria).ToListAsync();

            return query;
        }

        public bool Excluir(Alocação obj)
        {
            _context.Alocação.Remove(obj);
            return _context.SaveChanges() > 0;
        }

        public bool Incluir(Alocação obj)
        {
            _context.Alocação.Add(obj);
            return _context.SaveChanges() > 0;
        }
    }
}
