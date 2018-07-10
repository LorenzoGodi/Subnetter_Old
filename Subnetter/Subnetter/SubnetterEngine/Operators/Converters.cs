using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Operators
{
    class Converters
    {
        public static string AddressBinToInt(string address)
        {
            string[] _parts = Formatters.Divide(address);
            int[] parts = new int[4];

            for (int v = 0; v < 4; v++)
                parts[v] = Convert.ToInt32(_parts[v], 2);

            return Formatters.Merge(parts);
        }

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

        public static int SubnetmaskToSlash(string address)
        {
            int slash = 0;
            foreach (char chr in AddressToBin(address))
                if (chr == '1')
                    slash++;
            return slash;
        }

        public static string SubnetmaskSlashToBin(int slash)
        {
            string str = "";
            for (int v = 0; v < slash; v++)
                str += "1";
            return Formatters.AddPoints(Formatters.CompleteAddressHead(str));
        }

        public static string SubnetmaskSlashToInt(int slash)
        {
            return AddressBinToInt(SubnetmaskSlashToBin(slash));
        }
    }
}
