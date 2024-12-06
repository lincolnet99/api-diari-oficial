using System.Collections.Generic;

namespace Regulatorio.SharedKernel.Validations
{
    public interface IValidatorResponse
    {
        bool IsSuccess { get; }
        void AddError(string code, string message, string propertyName = "");
        void AddError(IList<Error> errors);
        IList<Error> Errors { get; set; }
    }
}