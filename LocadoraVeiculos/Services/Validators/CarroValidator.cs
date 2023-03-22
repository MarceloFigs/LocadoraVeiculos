using FluentValidation;
using LocadoraVeiculos.Models;
using System;

namespace LocadoraVeiculos.Services.Validators
{
    public class CarroValidator : AbstractValidator<Carro>
    {
        public CarroValidator()
        {
            RuleFor(n => n.Chassi).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(17).WithMessage("Precisa conter 17 caracteres");

            RuleFor(n => n.Cor).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .MaximumLength(50);

            RuleFor(n => n.Modelo).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .MaximumLength(50);

            RuleFor(n => n.Marca).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .MaximumLength(50);

            RuleFor(n => n.Placa).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(7).WithMessage("Precisa conter 7 caracteres");

            var anoAtual = DateTime.Now.Year;
            RuleFor(n => n.Ano).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .InclusiveBetween(1000, anoAtual).WithMessage("Valor invalido");
        }
    }
}
