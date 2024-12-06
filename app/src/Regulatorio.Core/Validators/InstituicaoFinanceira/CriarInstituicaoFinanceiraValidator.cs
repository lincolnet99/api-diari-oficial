using FluentValidation;
using Regulatorio.Domain.Request.InstituicaoFinanceira;

namespace Regulatorio.Core.Validators.InstituicaoFinanceira
{
    public class CriarInstituicaoFinanceiraValidator : AbstractValidator<CriarInstituicaoFinanceiraRequest>
    {
        public CriarInstituicaoFinanceiraValidator()
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
