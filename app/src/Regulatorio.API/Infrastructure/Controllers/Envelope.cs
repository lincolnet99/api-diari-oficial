

using Regulatorio.SharedKernel;

namespace Regulatorio.API.Infrastructure.Controllers
{
    public class Envelope<T>
    {
        public int Code { get; private set; }
        public T Result { get; }
        public bool IsSuccessful { get; }
        public IList<Error> Errors { get; }
        public DateTime TimeGenerated { get; private set; }
        public string Copyright => $"Copyright {DateTime.Now.Year} Tecnobank Tecnologia Bancária. All Rights Reserved";

        protected internal Envelope(int code, T result, IList<Error> errors)
        {
            Code = code;
            Result = result;
            Errors = errors;
            IsSuccessful = errors == null;
            TimeGenerated = DateTime.UtcNow;
        }
    }

    public class Envelope : Envelope<string>
    {
        protected Envelope(int code, IList<Error> errors)
            : base(code, null, errors)
        {
        }

        public static Envelope<T> Ok<T>(int code, T result)
        {
            return new Envelope<T>(code, result, null);
        }

        public static Envelope Ok(int code)
        {
            return new Envelope(code, null);
        }

        public static Envelope Error(int code, IList<Error> errors)
        {
            return new Envelope(code, errors);
        }

        public static Envelope Error(int code, Error error)
        {
            return new Envelope(code, new List<Error>() { error });
        }
    }
}