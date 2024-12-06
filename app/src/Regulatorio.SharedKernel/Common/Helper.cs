using System.Collections.Generic;
using System.Globalization;
using System;

namespace Regulatorio.SharedKernel.Common;

public static class Helper
{
    public static List<string> GerarIntervaloEntre(string dataInicio, string dataFim)
    {
        if (string.IsNullOrEmpty(dataInicio) || string.IsNullOrEmpty(dataFim))
            return null;

        var datas = new List<string>();

        const string format = "yyyy-MM-dd";

        var inicio = DateTime.ParseExact(dataInicio, format, CultureInfo.InvariantCulture);
        var fim = DateTime.ParseExact(dataFim, format, CultureInfo.InvariantCulture);

        var atual = inicio;

        while (atual <= fim)
        {
            var dataFormatada = atual.ToString("yyyy-MM-dd");

            datas.Add(dataFormatada);
            atual = atual.AddDays(1);
        }

        return datas;
    }

    public static string TransformarArrayEmString(string[] array, bool ehString)
    {
        var resultado = string.Empty;

        for (var i = 0; i < array.Length; i++)
        {
            if (ehString)
                resultado += $"'{array[i]}'";
            else
                resultado += $"{array[i]}";


            if (i < array.Length - 1)
                resultado += ", ";
        }

        return resultado;
    }
}