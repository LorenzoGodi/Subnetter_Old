using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Operators
{
    class Converters
    {
        /// <summary>
        /// Converte un indirizzo in base binaria in base decimale
        /// </summary>
        /// <param name="address">Indirizzo in base binaria</param>
        /// <returns>Indirizzo trasformato in base decimale</returns>
        public static string AddressBinToInt(string address)
        {
            string[] _parts = Formatters.Divide(address);
            int[] parts = new int[4];

            for (int v = 0; v < 4; v++)
                parts[v] = Convert.ToInt32(_parts[v], 2);

            return Formatters.Merge(parts);
        }

        /// <summary>
        /// Converte un indirizzo in base 10 in base binaria
        /// </summary>
        /// <param name="address">Indirizzo in base decimale</param>
        /// <returns>Indirizzo trasformato in base binaria</returns>
        public static string AddressIntToBin(string address)
        {
            string[] _parts = Formatters.Divide(address);
            string[] parts = new string[4];

            for (int v = 0; v < 4; v++)
                parts[v] = Convert.ToString(int.Parse(_parts[v]), 2);

            for (int v = 0; v < parts.Length; v++)
                while (parts[v].Length < 8)
                    parts[v] = "0" + parts[v];

            string result = Formatters.Merge(parts);
            if (result.Length != 35) { throw new Exception("Errore interno del codice - lunghezza della Subnetmask binaria non valida!"); }

            return result;
        }

        /// <summary>
        /// Converte un indirizzo in base 10 in base binaria, se è già in base binaria lo lascia invariato
        /// </summary>
        /// <param name="address">Indirizzo in base decimale o binaria</param>
        /// <returns>Indirizzo trasformato in base binaria</returns>
        public static string AddressToBin(string address)
        {
            switch (AI.DetermineAddressStructure(address))
            {
                case AddressStructure.BinaryAddress:
                    return address;
                default:
                    return AddressIntToBin(address);
            }
        }

        /// <summary>
        /// Data una subnetmask in base decimale o binaria, ritorna il suo valore in formato slash (numero di bit della parte network)
        /// </summary>
        /// <param name="address">Subnetmask in base decimale o binaria</param>
        /// <returns>Slash (numero di bit della parte network)</returns>
        public static int SubnetmaskToSlash(string address)
        {
            int slash = 0;
            foreach (char chr in AddressToBin(address))
                if (chr == '1')
                    slash++;
            return slash;
        }

        /// <summary>
        /// Dato lo slash di una subnetmask, calcola la subnetmask in base binaria
        /// </summary>
        /// <param name="slash">Slash (numero di bit della parte network)</param>
        /// <returns>Subnetmask in base binaria</returns>
        public static string SubnetmaskSlashToBin(int slash)
        {
            string str = "";
            for (int v = 0; v < slash; v++)
                str += "1";
            return Formatters.AddPoints(Formatters.CompleteAddressHead(str));
        }

        /// <summary>
        /// Dato lo slash di una subnetmask, calcola la subnetmask in base decimale
        /// </summary>
        /// <param name="slash">Slash (numero di bit della parte network)</param>
        /// <returns>Subnetmask in base decimale</returns>
        public static string SubnetmaskSlashToInt(int slash)
        {
            return AddressBinToInt(SubnetmaskSlashToBin(slash));
        }
    }
}
