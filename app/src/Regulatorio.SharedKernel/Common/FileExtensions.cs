using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;

namespace Regulatorio.SharedKernel.Common
{
    public static class FileExtensions
    {
        public static string ExtrairExtensao(string arquivoBase64)
        {
            string extension = arquivoBase64.Substring(0, 5);
            switch (extension.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "R0lGO":
                    return ".gif";
                case "JVBER":
                    return ".pdf";
                case "UESDB":
                    return ".docx";
                case "0M8R4":
                    return ".doc";
                default:
                    return string.Empty;
            };
        }

        public static string RemoveHeaderBase64(string arquivoBase64)
        {
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(arquivoBase64, "");
            data = new Regex(@"^data:application\/[a-z]+;base64,").Replace(data, "");
            return data;
        }

        public static int Tamanho(string arquivoBase64)
        {
            var documentoByteArray = Convert.FromBase64String(arquivoBase64);
            return documentoByteArray.Length;
        }

        public static async Task<string> ConveterArquivoParaBase64(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }

    }
}
