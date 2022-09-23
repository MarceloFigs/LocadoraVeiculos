using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class LocadoraVeiculosContext : DbContext
    {
        public LocadoraVeiculosContext(DbContextOptions options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("DefaultConnection");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alocação>().HasKey(a => new { a.Cpf, a.Chassi });
            modelBuilder.Entity<Alocação>()
                .HasOne(c => c.Cliente)
                .WithMany(a => a.Alocação)
                .HasForeignKey(c => c.Cpf);
            modelBuilder.Entity<Alocação>()
                .HasOne(c => c.Carro)
                .WithMany(a => a.Alocação)
                .HasForeignKey(c => c.Chassi);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Alocação> Alocação { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
