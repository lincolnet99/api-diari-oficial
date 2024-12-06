using FluentValidation;
using FluentValidation.Internal;
using Regulatorio.SharedKernel;

namespace Regulatorio.Core.Validators
{
    public static class ValidatorWrapper
    {
        public static Result ValidateRequest<T>(this IValidator<T> validator, T instance, Action<ValidationStrategy<T>> options)
        {
            var result = validator.Validate(ValidationContext<T>.CreateWithOptions(instance, options));

            var errors = result.Errors
                .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
                .ToList();

            return errors.Any() ? Result.Fail(new List<Error>(errors)) : Result.Ok();
        }

        public static Result ValidateRequest<T>(this IValidator<T> validator, T instance)
        {
            var result = validator.Validate(new ValidationContext<T>(instance));

            var errors = result.Errors
                .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage, failure.PropertyName))
                .ToList();

            return errors.Any() ? Result.Fail(new List<Error>(errors)) : Result.Ok();
        }
    }
}
