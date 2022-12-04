using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Models
{
    internal static class Extension
    {

        public static string Capitalize(this string text)
        {
            text = text.Trim();
            StringBuilder sb = new StringBuilder();
            if (text == null || text == "")
            {
                return "";
            }
            if (text.Contains(" "))
            {
                string[] parts = text.Split(" ");
                for (int i = 0; i < parts.Length; i++)
                {
                    sb.Append(char.ToUpper(parts[i][0])+ parts[i].Substring(1).ToLower() + " ");
                }
            }
            else
            {
                sb.Append(char.ToUpper(text[0]) + text.Substring(1).ToLower());
            }
            return sb.ToString();
        }
    }
}
