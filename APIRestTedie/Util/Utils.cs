using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace APIRestTedie.Util
{
    public static class Utils
    {
        static trampowEntidades context = new trampowEntidades();
        /// <summary>
        /// Valida o token usado nas chamadas do webservice
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ValidateToken(string token)
        {
            var credentials = Utils.Base64Decode(token);

            return credentials != "false" ?
                (from p in context.USUARIO
                 where p.EMAIL == credentials select 1
                        ).Any() : false;
        }
        /// <summary>
        /// Método auxiliar para remover acentos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = null;
            try
            {
                 base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);

            }catch(Exception)
            {
                return "false";
            }
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}