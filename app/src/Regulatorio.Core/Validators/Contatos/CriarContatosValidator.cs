using FluentValidation;
using Regulatorio.Domain.Request.Contatos;

namespace Regulatorio.Core.Validators.Contatos
{
    public class CriarContatosValidator : AbstractValidator<CriarContatoRequest>
    {
        public CriarContatosValidator()
        {

            RuleFor(t => t.Nome)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("Nome Vazio ou nulo");

            RuleFor(t => t.Nome)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Nome Vazio ou nulo");

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
