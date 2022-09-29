using FluentValidation;
using LocadoraVeiculos.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;

namespace LocadoraVeiculos.Services
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(n => n.Cpf).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(11).WithMessage("Precisa conter 11 digitos")
                .Must(cpf => Regex.IsMatch(cpf, "^[0-9]*$")).WithMessage("Número invalido")
                .Must(cpf => ValidaCPF(cpf)).WithMessage("CPF inválido");

                RuleFor(n => n.RG).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(7,11).WithMessage("Precisa entre 7 e 11 digitos")
                .Must(rg => Regex.IsMatch(rg, "^[0-9]*$")).WithMessage("Número invalido")
                .WithMessage("RG inválido");

            RuleFor(n => n.Nome).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(2,100)
                .WithMessage("Nome inválido");
            
            RuleFor(n => n.DtNascimento).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                .WithMessage("Cliente não é maior de idade");

            RuleFor(n => n.Cnh).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(11).WithMessage("Precisa conter 11 digitos")
                .WithMessage("Cnh inválida");

            RuleFor(n => n.Endereço)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(2, 100)
                .WithMessage("Endereço inválido");
        }

        private bool ValidaCPF(string cpf)
        {
            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" ||
                cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" ||
                cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" ||
                cpf == "99999999999")
            {
                return false;                
            }
            else
            {
                int multiplicador = 10;
                int multiplicador2 = 11;
                int resultado = 0;
                int resultado2 = 0;

                for (int i = 0; i < 9; i++)
                {
                    int digito = int.Parse(cpf.Substring(i, 1));
                    resultado += digito * multiplicador;
                    resultado2 += digito * multiplicador2;
                    multiplicador -= 1;
                    multiplicador2 -= 1;
                }
                int divisao = resultado % 11;

                int digitoVerificador;
                if (divisao < 2)
                {
                    digitoVerificador = 0;
                }
                else
                {
                    digitoVerificador = 11 - divisao;
                }
                resultado2 += digitoVerificador * multiplicador2;
                int divisao2 = resultado2 % 11;

                int digitoVerificador2;
                
                if (divisao2 < 2)
                    digitoVerificador2 = 0;
                else                
                    digitoVerificador2 = 11 - divisao2;                

                var verificador = string.Concat(digitoVerificador, digitoVerificador2);
                var ultimosDigitos = cpf.Substring(9);
                if (!verificador.Equals(ultimosDigitos))
                {
                    return false;
                }

                return true;
            }
        }

    }
}
