using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class AlocaçãoRepository : IAlocaçãoRepository
    {
        private readonly LocadoraVeiculosContext _context;
        public AlocaçãoRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public void Atualizar(Alocação obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }
        public Alocação BuscarAlocação(string cpf, string chassi)
        {
            var query = _context.Alocação.Where(a => a.Cpf == cpf && a.Chassi == chassi)
                .Include(c => c.Cliente)
                .Include(c => c.Carro)
                .FirstOrDefault();
            
            return query;
        }
        public Alocação BuscarPorId(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Alocação> BuscarTodos()
        {
            return _context.Alocação.ToList();
        }

        public void Excluir(Alocação obj)
        {
            _context.Alocação.Remove(obj);
            _context.SaveChanges();
        }

        public void Incluir(Alocação obj)
        {
            _context.Alocação.Add(obj);
            _context.SaveChanges();
        }
    }
}
