using FluentValidation;
using LocadoraVeiculos.Models;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace LocadoraVeiculos.Services.Validators
{
    public class AlocaçãoValidator : AbstractValidator<Alocação>
    {
        public AlocaçãoValidator()
        {
            RuleFor(n => n.Cpf).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(11).WithMessage("Precisa conter 11 digitos");

            RuleFor(n => n.Chassi).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .Length(17).WithMessage("Precisa conter 17 caracteres");

            RuleFor(dt => dt.DtSaida).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull().WithMessage("Campo não preenchido")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Data de saida deve ser igual ou maior que data atual")
                .LessThan(dt => dt.DtEntrega).WithMessage("Data de saida deve ser anterior a data de entrega");

            RuleFor(dt => dt.DtEntrega).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull().WithMessage("Campo não preenchido")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Data de saida deve ser igual ou maior que data atual");
        }
    }
}
