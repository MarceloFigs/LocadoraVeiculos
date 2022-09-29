using FluentValidation;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Services
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(n => n.Descrição).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .MaximumLength(50);

            RuleFor(n => n.ValorDiaria).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Campo não preenchido")
                .InclusiveBetween(100,10000).WithMessage("Valor deve ser entre 100 e 1000");                
        }
    }
}
