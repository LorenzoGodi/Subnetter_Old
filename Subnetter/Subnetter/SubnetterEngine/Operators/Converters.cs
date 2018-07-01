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
            string[] _parts = address.Split('.');
            int[] parts = new int[4];

            for (int v = 0; v < 4; v++)
                parts[v] = Convert.ToInt32(_parts[v]);

            return parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
        }

        public static string AddressIntToBin(string address)
        {
            string[] _parts = address.Split('.');
            int[] parts = new int[4];

            for (int v = 0; v < 4; v++)
                parts[v] = Convert.ToInt32(_parts[v], 2);

            return parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
        }
    }
}
