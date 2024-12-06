using FluentValidation;
using Regulatorio.Domain.Request.Registradoras;

namespace Regulatorio.Core.Validators.Registradora
{
    public class CriarRegistradoraValidator : AbstractValidator<CriarRegistradoraRequest>
    {
        public CriarRegistradoraValidator()
        {
            RuleFor(t => t.Uf)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("Uf Vazia não permitida");

            RuleFor(t => t.Uf)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Uf nula não permitida");

            //RuleFor(t => t.IdEmpresa)
            //    .NotNull()
            //    .WithErrorCode("400")
            //    .WithMessage(" empresa não permitida");


        }


    }
}
