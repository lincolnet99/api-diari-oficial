using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Regulatorio.SharedKernel.Common
{
    public static class EnumExtensions
    {
        public static string GetDescription(Enum value)
        {
            if (value is null)
                return "";

            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
                return "";

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}