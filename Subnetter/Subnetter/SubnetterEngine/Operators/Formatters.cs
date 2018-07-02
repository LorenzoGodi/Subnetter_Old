using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Operators
{
    class Formatters
    {
        public static string RemovePoints(string address) => address.Replace(".", "");

        public static string AddPoints(string address) => address.Substring(0, 8) + "." + address.Substring(8, 8) + "." + address.Substring(16, 8) + "." + address.Substring(24, 8);

        public static string[] Divide(string address) => address.Split('.');

        public static string Merge(List<string> parts, string divisor = ".")
        {
            string result = parts[0].ToString();
            for (int v = 1; v < parts.Count; v++)
                result = result + divisor + parts[v].ToString();
            return result;
        }
        public static string Merge(string[] parts, string divisor = ".")
        {
            string result = parts[0];
            for (int v = 1; v < parts.Length; v++)
                result = result + divisor + parts[v];
            return result;
        }
        public static string Merge(int[] parts, string divisor = ".")
        {
            string result = parts[0].ToString();
            for (int v = 1; v < parts.Length; v++)
                result = result + divisor + parts[v].ToString();
            return result;
        }

        public static string CompleteAddressHead(string address, char bit = '0')
        {
            if(bit != '1' && bit != '0') { throw new Exception("Bit non valido"); }
            while (address.Length < 32)
                address = address + bit.ToString();
            return address;
        }
    }
}
