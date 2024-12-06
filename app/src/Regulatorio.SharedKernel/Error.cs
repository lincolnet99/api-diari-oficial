using System;

namespace Regulatorio.SharedKernel
{
    [Serializable]
    public sealed class Error
    {
        public Error()
            : this(string.Empty, string.Empty, string.Empty)
        {
        }

        public Error(string code, string message, string propertyName = "")
        {
            Code = code;
            Message = message;
            PropertyName = propertyName;
        }

        public string Code { get; }
        public string Message { get; }
        public string PropertyName { get; }
    }
}