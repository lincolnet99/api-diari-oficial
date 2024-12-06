using FluentValidation;
using Regulatorio.Domain.Request.Normativos;

namespace Regulatorio.Core.Validators.Normativos
{
    public class CriarNormativoValidator : AbstractValidator<CriarNormativoRequest>
    {
        public CriarNormativoValidator()
        {
            RuleFor(t => t.Uf)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("Uf Vazia não permitida")
                .When(t => t.VisaoNacional == false);

            RuleFor(t => t.Uf)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Uf nula não permitida")
                .When(t => t.VisaoNacional == false);
        }
    }
}
