using FluentValidation;
using Regulatorio.Domain.Request.Registros;

namespace Regulatorio.Core.Validators.Registros
{
    public class CriarRegistrosValidator : AbstractValidator<CriarRegistroRequest>
    {
        public CriarRegistrosValidator()
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
