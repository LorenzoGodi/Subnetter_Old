using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Operators
{
    class Formatters
    {
        /// <summary>
        /// Rimuove i punti '.' contenuti in un indirizzo
        /// </summary>
        public static string RemovePoints(string address) => address.Replace(".", "");

        /// <summary>
        /// Aggiunge i punti a un idirizzo in base binaria dividendo in gruppi da 8 bit
        /// </summary>
        public static string AddPoints(string address) => address.Substring(0, 8) + "." + address.Substring(8, 8) + "." + address.Substring(16, 8) + "." + address.Substring(24, 8);

        /// <summary>
        /// Aggiunge i punti a un idirizzo in base binaria [diviso in parti di una lista] dividendo in gruppi da 8 bit
        /// </summary>
        public static List<string> AddPointsToParts(List<string> addressParts)
        {
            int[] divisori = { 8, 17, 26 };
            List<string> result = new List<string>();

            int pointer = 0;
            for(int part = 0; part < addressParts.Count; part++)
            {
                // Per ogni parte di lista
                string newPart = "";
                for(int chr_part = 0; chr_part < addressParts[part].Length; chr_part++)
                {
                    // Per ogni carattere della parte
                    if (divisori.Contains(pointer))
                    {
                        if (chr_part > 0)
                            newPart += ".";
                        else
                            result.Add(".");
                        pointer++;
                    }
                    newPart += addressParts[part][chr_part];
                    pointer++;
                }
                result.Add(newPart);
            }
            return result;
        }

        /// <summary>
        /// Divide un indirizzo nelle quattro parti divise da un punto
        /// </summary>
        public static string[] Divide(string address) => address.Split('.');

        /// <summary>
        /// Esegue il merge delle parti di un indirizzo
        /// </summary>
        /// <param name="parts">Parti dell'indirizzo</param>
        /// <param name="divisor">Divisore ('.' di default)</param>
        /// <returns>Indirizzo in formato normale</returns>
        public static string Merge(List<string> parts, string divisor = ".")
        {
            string result = parts[0].ToString();
            for (int v = 1; v < parts.Count; v++)
                result = result + divisor + parts[v].ToString();
            return result;
        }

        /// <summary>
        /// Esegue il merge delle parti di un indirizzo
        /// </summary>
        /// <param name="parts">Parti dell'indirizzo</param>
        /// <param name="divisor">Divisore ('.' di default)</param>
        /// <returns>Indirizzo in formato normale</returns>
        public static string Merge(string[] parts, string divisor = ".")
        {
            string result = parts[0];
            for (int v = 1; v < parts.Length; v++)
                result = result + divisor + parts[v];
            return result;
        }

        /// <summary>
        /// Esegue il merge delle parti di un indirizzo
        /// </summary>
        /// <param name="parts">Parti dell'indirizzo</param>
        /// <param name="divisor">Divisore ('.' di default)</param>
        /// <returns>Indirizzo in formato normale</returns>
        public static string Merge(int[] parts, string divisor = ".")
        {
            string result = parts[0].ToString();
            for (int v = 1; v < parts.Length; v++)
                result = result + divisor + parts[v].ToString();
            return result;
        }

        /// <summary>
        /// Completa un elenco di bit con 0 o 1
        /// </summary>
        public static string CompleteAddressHead(string address, char bit = '0')
        {
            if(bit != '1' && bit != '0') { throw new Exception("Bit non valido"); }
            while (address.Length < 32)
                address = address + bit.ToString();
            return address;
        }
    }
}
