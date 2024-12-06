using System;

namespace Regulatorio.SharedKernel.Common
{
    public static class StringExtensions
    {
        public static string AnonimizarString(this string value, bool completo = false)
        {
            string retorno = "";
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (!completo)
            {
                if (value.Length > 2)
                {
                    var array = value.ToCharArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i < 2) continue;
                        if (i == array.Length - 2 || i == array.Length - 1) continue;
                        array[i] = '*';
                    }

                    foreach (var item in array)
                        retorno += item;
                }
                else
                    retorno = "**";
            }
            else
            {
                var array = value.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = '*';

                foreach (var item in array)
                    retorno += item;
            }
            return retorno;
        }

        public static string AnonimizarNome(this string value, bool completo = false)
        {
            string retorno = "";
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (!completo)
            {
                if (value.Length > 3)
                {
                    var array = value.ToCharArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i < 3) continue;
                        if (i == array.Length - 3 || i == array.Length - 2 || i == array.Length - 1) continue;
                        if (array[i] != 32)
                            array[i] = '*';
                    }

                    foreach (var item in array)
                        retorno += item;
                }
                else
                    retorno = "**";
            }
            else
            {
                var array = value.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = '*';

                foreach (var item in array)
                    retorno += item;
            }
            return retorno;
        }

        public static string AnonimizarDocumento(this string value, bool completo = false)
        {
            string retorno = "";
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (!completo)
            {
                if (value.Length > 3)
                {
                    switch (value.Length)
                    {
                        case 14: //cnpj
                            value = Convert.ToUInt64(value).ToString(@"00\.000\.000\/0000\-00");
                            break;
                        case 11: //cpf
                            value = Convert.ToUInt64(value).ToString(@"000\.000\.000\-00");
                            break;
                        case 8: //cep
                            value = Convert.ToUInt64(value).ToString(@"00\.000\-000");
                            break;
                        case 9: //cep
                            value = Convert.ToUInt64(value).ToString(@"00000\-0000");
                            break;
                        default:
                            break;
                    }

                    var array = value.ToCharArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i < 2) continue;
                        if (i == array.Length - 2 || i == array.Length - 1) continue;
                        if (array[i] != 32 && array[i] != 45 && array[i] != 46 && array[i] != 47)
                            array[i] = '*';
                    }

                    foreach (var item in array)
                        retorno += item;
                }
                else
                    retorno = "**";
            }
            else
            {
                var array = value.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = '*';

                foreach (var item in array)
                    retorno += item;
            }
            return retorno;
        }
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}
