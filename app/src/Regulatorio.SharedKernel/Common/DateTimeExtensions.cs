using System;
using System.Globalization;

namespace Regulatorio.SharedKernel.Common
{
    public static class DateTimeExtensions
    {
        public static string ConvertStringFormat(DateTime? date)
        {
            if (date == null)
                return string.Empty;

            return date.Value.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string ConvertStringFormatOrNull(DateTime? date)
        {
            if (date == null)
                return null;

            return date.Value.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static int DiffDateTime(DateTime dataInicio, DateTime dataFim)
        {
            return (dataFim - dataInicio).Days;
        }

        public static string GetTimeOfDateTime(DateTime? date)
        {
            if (date == null)
                return string.Empty;

            if (date == DateTime.MinValue)
                return string.Empty;

            return date.Value.ToString("HH:mm");
        }

        public static string GetDateOfDateTime(DateTime? date)
        {
            if (date == null)
                return string.Empty;

            if (date == DateTime.MinValue)
                return string.Empty;

            return date.Value.ToString("dd/MM/yyyy");
        }

        public static string GetDateOfDateTime(DateTime? date, string formato)
        {
            if (date == null)
                return string.Empty;

            if (date == DateTime.MinValue)
                return string.Empty;

            return date.Value.ToString(formato);
        }

        public static DateTime ToDateTime(string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                throw new InvalidOperationException();

            if (!DateTime.TryParse(str, CultureInfoDefault.CultureDateTime, DateTimeStyles.None, out var datetime))
                throw new InvalidOperationException();

            if (datetime == DateTime.MinValue)
                throw new InvalidOperationException();

            return datetime;
        }

        public static DateTime? DateTimeOrNull(string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return null;

            if (!DateTime.TryParse(str, CultureInfoDefault.CultureDateTime, DateTimeStyles.None, out var datetime))
                return null;

            if (datetime == DateTime.MinValue)
                return null;

            return datetime;
        }

        public static bool isValidDate(string str)
        {
            if (DateTime.TryParse(str, CultureInfoDefault.CultureDateTime, DateTimeStyles.None, out var datetime))
                return true;

            return false;
        }
    }

    public struct DateTimeString
    {
        private string _value;

        private DateTimeString(DateTime dateTime)
        {
            if (dateTime == default)
                _value = string.Empty;

            _value = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static implicit operator DateTimeString(DateTime? dateTime) => new(dateTime.GetValueOrDefault());
        public static implicit operator DateTimeString(DateTime dateTime) => new(dateTime);
        public override string ToString() => _value;
    }
}