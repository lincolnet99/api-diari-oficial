using FluentValidation;
using Regulatorio.Domain.Request.Garantias;

namespace Regulatorio.Core.Validators.Garantias
{
    public class CriarGarantiasValidator : AbstractValidator<CriarGarantiaRequest>
    {
        public CriarGarantiasValidator()
        {
            RuleFor(t => t.Uf)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("Uf Vazia não permitida");

            RuleFor(t => t.Uf)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Uf nula não permitida");
        }
    }
}
