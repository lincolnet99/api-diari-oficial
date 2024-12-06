using System.Text.RegularExpressions;

namespace Regulatorio.SharedKernel
{
    public static class Extensions
    {
        public static void Guard(this object obj, string message, string paramName)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName, message);
        }

        public static void Guard(this string str, string message, string paramName)
        {
            if (str == null)
                throw new ArgumentNullException(paramName, message);

            if (string.IsNullOrEmpty(str))
                throw new ArgumentException(message, paramName);
        }

        public static void Guard(this Guid guid, string message, string paramName)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException(message, paramName);
        }

        public static bool ValidarCpf(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (cpf.Equals("00000000000") ||
                cpf.Equals("11111111111") ||
                cpf.Equals("22222222222") ||
                cpf.Equals("33333333333") ||
                cpf.Equals("44444444444") ||
                cpf.Equals("55555555555") ||
                cpf.Equals("66666666666") ||
                cpf.Equals("77777777777") ||
                cpf.Equals("88888888888") ||
                cpf.Equals("99999999999"))
            {
                return false;
            }

            var tempCpf = cpf[..9];
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCpf += digito;

            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto;

            return cpf.EndsWith(digito);
        }

        public static bool ValidarCnpj(this string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            if (cnpj.Equals("00000000000000") ||
                cnpj.Equals("11111111111111") ||
                cnpj.Equals("22222222222222") ||
                cnpj.Equals("33333333333333") ||
                cnpj.Equals("44444444444444") ||
                cnpj.Equals("55555555555555") ||
                cnpj.Equals("66666666666666") ||
                cnpj.Equals("77777777777777") ||
                cnpj.Equals("88888888888888") ||
                cnpj.Equals("99999999999999"))
            {
                return false;
            }

            var tempCnpj = cnpj[..12];
            var soma = 0;

            for (var i = 0; i < 12; i++)
                soma += (tempCnpj[i] - '0') * multiplicador1[i];

            var resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCnpj += digito;

            soma = 0;

            for (var i = 0; i < 13; i++)
                soma += (tempCnpj[i] - '0') * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto;

            return cnpj.EndsWith(digito);
        }

        public static string KeepOnlyNumbers(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"[^\d]", "");
        }
    }
}