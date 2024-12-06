using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Regulatorio.SharedKernel.Validations;

namespace Regulatorio.SharedKernel.Responses
{
    public abstract class BaseResponse : IValidatorResponse, IResponse
    {
        protected BaseResponse()
        {
            Errors = new List<Error>();
        }

        public void AddError(string code, string message, string propertyName = "") => Errors.Add(new Error(code, message, propertyName));

        public void AddError(IList<Error> errors)
        {
            foreach (var error in errors)
                AddError(error.Code, error.Message, error.PropertyName);
        }

        [JsonIgnore]
        public bool IsSuccess => !Errors.Any();

        [JsonIgnore]
        public IList<Error> Errors { get; set; }
    }
}