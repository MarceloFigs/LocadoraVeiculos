using LocadoraVeiculos.Models;
using System.Linq;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly LocadoraVeiculosContext _context;

        public ClienteRepository(LocadoraVeiculosContext context)
        {
            _context = context;
        }
        public void Incluir(Cliente obj)
        {
            _context.Cliente.Add(obj);
            _context.SaveChanges();
        }
        public void Excluir(Cliente obj)
        {
            _context.Cliente.Remove(obj);
            _context.SaveChanges();
        }
        public void Atualizar(Cliente obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }
        public Cliente BuscarPorId(string cpf)
        {
            return _context.Cliente.FirstOrDefault(c => c.Cpf == cpf);
        }
    }
}
