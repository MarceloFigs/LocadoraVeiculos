using Bogus;
using Bogus.Extensions.Brazil;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocadoraVeiculos.IntegrationTests.Creator
{
    public class ClienteCreator
    {
        private readonly LocadoraVeiculosContext _context;

        public ClienteCreator(LocadoraVeiculosContext context)
        {
            _context = context;
        }

        public async Task AddClientesAsync()
        {
            var fakeClientes = new Faker<Cliente>("pt_BR")
                .RuleFor(x => x.Cpf, f => f.Person.Cpf(false))
                .RuleFor(x => x.Nome, f => f.Person.FirstName)
                .RuleFor(x => x.DtNascimento, f => f.Person.DateOfBirth.AddYears(-18))
                .RuleFor(x => x.RG, f => f.Random.Long(10000000000, 99999999999).ToString())
                .RuleFor(x => x.Cnh, f => f.Random.Long(10000000000, 99999999999).ToString())
                .RuleFor(x => x.Cidade, f => f.Person.Address.City)
                .RuleFor(x => x.Logradouro, f => f.Person.Address.Street)
                .RuleFor(x => x.UF, f => f.Person.Address.State)
                .RuleFor(x => x.CEP, f => int.Parse(Regex.Replace(f.Person.Address.ZipCode.ToString(), @"(\s+|-)", "")))
                .Generate(5);

            await _context.AddRangeAsync(fakeClientes);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> CreateClienteAsync()
        {
            var fakeCliente = new Faker<Cliente>("pt_BR")
                .RuleFor(x => x.Cpf, f => f.Person.Cpf(false))
                .RuleFor(x => x.Nome, f => f.Person.FirstName)
                .RuleFor(x => x.DtNascimento, f => f.Person.DateOfBirth.AddYears(-18))
                .RuleFor(x => x.RG, f => f.Random.Long(10000000000, 99999999999).ToString())
                .RuleFor(x => x.Cnh, f => f.Random.Long(10000000000, 99999999999).ToString())
                .RuleFor(x => x.Cidade, f => f.Person.Address.City)
                .RuleFor(x => x.Logradouro, f => f.Person.Address.Street)
                .RuleFor(x => x.UF, f => f.Person.Address.State)
                .RuleFor(x => x.CEP, f => int.Parse(Regex.Replace(f.Person.Address.ZipCode.ToString(), @"(\s+|-)", "")))
                .Generate();

            await _context.AddAsync(fakeCliente);
            await _context.SaveChangesAsync();
            return fakeCliente;
        }

        public static Cliente CreateCliente()
        {
            var fakeCliente = new Faker<Cliente>("pt_BR")
                .RuleFor(x => x.Cpf, f => f.Person.Cpf(false))
                .RuleFor(x => x.Nome, f => f.Person.FirstName)
                .RuleFor(x => x.DtNascimento, f => f.Person.DateOfBirth.AddYears(-18))
                .RuleFor(x => x.RG, f => f.Random.Long(10000000000, 99999999999).ToString())
                .RuleFor(x => x.Cnh, f => f.Random.Long(10000000000, 99999999999).ToString())
                .RuleFor(x => x.Cidade, f => f.Person.Address.City)
                .RuleFor(x => x.Logradouro, f => f.Person.Address.Street)
                .RuleFor(x => x.UF, f => f.Person.Address.State)
                .Generate();

            fakeCliente.CEP = 11520010;
            return fakeCliente;
        }
        public Cliente AtualizaCliente(Cliente clienteAtualizar)
        {
            clienteAtualizar.CEP = 11065001;
            clienteAtualizar.Logradouro = "Avenida Doutor Bernardino de Campos";
            clienteAtualizar.UF = "SP";
            clienteAtualizar.Cidade = "Santos";

            return clienteAtualizar;
        }
    }
}
