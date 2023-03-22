using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LocadoraVeiculos.Repository.EFCore
{
    public class LocadoraVeiculosContext : DbContext
    {
        public LocadoraVeiculosContext(DbContextOptions options) : base(options)
        {

        }
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

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Cpf = "05642334395",
                    RG = "12345678900",
                    DtNascimento = DateTime.Parse("03-06-1999"),
                    Cnh = "99988877700",
                    Nome = "Eddie",
                    CEP = 11520010
                }
                );
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    CodCategoria = 1,
                    Descrição = "carro para viagem",
                    ValorDiaria = 1000
                }
                );
            modelBuilder.Entity<Carro>().HasData(
                new Carro
                {
                    Chassi = "6AbE1cS9b4N5D1711",
                    Cor = "amarelo",
                    Modelo = "compacto",
                    Marca = "Ford",
                    Placa = "xyz9999",
                    Ano = 2015,
                    CodCategoria = 1
                }
                );
            modelBuilder.Entity<Alocação>().HasData(
                new Alocação
                {
                    Cpf = "05642334395",
                    Chassi = "6AbE1cS9b4N5D1711",
                    DtSaida = DateTime.Now.AddDays(1),
                    DtEntrega = DateTime.Now.AddDays(10)
                }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Alocação> Alocação { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
