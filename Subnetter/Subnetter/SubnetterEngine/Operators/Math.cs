using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Operators
{
    class Math
    {
        public static string Increment(string address)
        {
            address = Converters.AddressToBin(address);
            if(address == "11111111.11111111.11111111.11111111") { throw new Exception("Non incrementabile"); }
            address = address.Replace(".", "");
            //
            int temp = address.Length;
            while (address[address.Length - 1] == 1)
            {
                address = address.Remove(address.Length - 1);
            }
            address = address.Remove(address.Length - 1);
            address = address + "1";
            while (address.Length < temp)
                address = address + "0";
            //
            address = address.Substring(0, 8) + "." + address.Substring(8, 8) + "." + address.Substring(16, 8) + "." + address.Substring(24, 8);
            return address;
        }

        public static string Decrement(string address)
        {
            address = Converters.AddressToBin(address);
            if (address == "00000000.00000000.00000000.00000000") { throw new Exception("Non decrementabile"); }
            address = address.Replace(".", "");
            //
            int temp = address.Length;
            while (address[address.Length - 1] == 0)
            {
                address = address.Remove(address.Length - 1);
            }
            address = address.Remove(address.Length - 1);
            address = address + "0";
            while (address.Length < temp)
                address = address + "1";
            //
            address = address.Substring(0, 8) + "." + address.Substring(8, 8) + "." + address.Substring(16, 8) + "." + address.Substring(24, 8);
            return address;
        }
    }
}
