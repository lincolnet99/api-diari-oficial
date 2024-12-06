using FluentValidation;
using Regulatorio.Domain.Request.Registradoras;

namespace Regulatorio.Core.Validators.Registradora
{
    public class CriarRegistradorasValidator : AbstractValidator<List<CriarRegistradoraRequest>>
    {
        public CriarRegistradorasValidator()
        {
            RuleForEach(t => t)
                .SetValidator(new CriarRegistradoraValidator());

        }


    }
}
