using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly LocadoraVeiculosContext _context;

        public ClienteRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public bool Incluir(Cliente obj)
        {
            _context.Cliente.Add(obj);
            return _context.SaveChanges() > 0;
        }
        public bool Excluir(Cliente obj)
        {
            _context.Cliente.Remove(obj);
            return _context.SaveChanges() > 0;
        }
        public bool Atualizar(Cliente obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            var resultado = _context.SaveChanges() > 0;
            _context.Entry(obj).State = EntityState.Detached;
            return resultado;
        }
        public async Task<Cliente> BuscarPorIdAsync(string cpf)
        {
            return await _context.Cliente.FirstOrDefaultAsync(c => c.Cpf == cpf);            
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosAsync()
        {
            return await _context.Cliente.ToListAsync();
        }
    }
}
