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

        public void AtualizarCliente(Cliente cliente)
        {
            _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        public Cliente GetClienteByCpf(string cpf)
        {
            return _context.Cliente.FirstOrDefault(c => c.Cpf == cpf);
        }

        public void CadastrarCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
        }

        public void ExcluirCliente(Cliente cliente)
        {
            _context.Cliente.Remove(cliente);
            _context.SaveChanges();
        }
    }
}
