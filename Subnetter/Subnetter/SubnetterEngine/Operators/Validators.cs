using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subnetter.SubnetterEngine.Objects;

namespace Subnetter.SubnetterEngine.Operators
{
    class Validators
    {
        /// <summary>
        /// Determina se un indirizzo di rete ha i requisiti per essere valido in base alla sua tipologia
        /// </summary>
        /// <param name="address">Indirizzo di rete da verificare</param>
        /// <param name="type">Tipologia di indirizzo di rete</param>
        /// <param name="structure">Struttura dell'indirizzo di rete</param>
        /// <returns></returns>
        public static bool IsValidAddress(string address, AddressType type, AddressStructure structure)
        {
            if (type == AddressType.NetworkAddress)
            {
                if (structure == AddressStructure.IntegerAddress)
                    return _IsValidAddressInt(address);
                else
                    return _IsValidAddressBin(address);
            }
            else
            {
                if (structure == AddressStructure.IntegerAddress)
                    return _IsValidSubnetmaskInt(address);
                else
                    return _IsValidSubnetmaskBin(address);
            }
        }        

        /// <summary>
        /// Determina se l'indirizzo dato è un indirizzo di rete valido facendo riferimento alla subnetmask fornita
        /// </summary>
        /// <param name="address">Indirizzo di rete da verificare</param>
        /// <param name="subnetmask">Subnetmask di riferimento</param>
        public static bool IsValidAddressNetwork(string address, string subnetmask)

        {
            address = Converters.AddressToBin(address);
            subnetmask = Converters.AddressToBin(subnetmask);

            bool ok = true;
            for (int v = 0; v < address.Length; v++)
                if (subnetmask[v] == '0')
                    ok = address[v] == '0' ? ok : false;

            return ok;
        }

        /// <summary>
        /// Determina se l'indirizzo dato è un potenziale indirizzo di rete valido
        /// </summary>
        /// <param name="address">Indirizzo di rete da verificare</param>
        public static NetworkAddressStatus NetAddStat(string address)
        {
            if (address[address.Length - 1] == 0)
                if (address[address.Length - 2] == 0)
                    return NetworkAddressStatus.Valid;
                else return NetworkAddressStatus.OnlyTwoHosts;
            else return NetworkAddressStatus.NotValid;
        }

        //

        public static bool _IsValidAddressBin(string addr)
        {
            string valid = "10.";
            bool ok = true;

            if (ok = (!string.IsNullOrEmpty(addr)) && addr.Contains(".") && addr.Split('.').Length == 4)
            {
                foreach (char chr in addr)
                    ok = valid.Contains(chr) ? ok : false;

                foreach (string str in Formatters.Divide(addr))
                {
                    ok = str.Length == 8 ? ok : false;
                }

                foreach (string str in Formatters.Divide(addr))
                {
                    ok = (str != "") ? (_IsValidAddressNumber(Convert.ToInt32(str, 2).ToString()) ? ok : false) : false;
                }
            }

            return ok;
        }

        public static bool _IsValidAddressInt(string addr)
        {
            string valid = "1234567890.";
            bool ok = true;

            if (ok = (!string.IsNullOrEmpty(addr)) && addr.Contains(".") && addr.Split('.').Length == 4)
            {
                foreach (char chr in addr)
                    ok = valid.Contains(chr) ? ok : false;

                foreach (string str in Formatters.Divide(addr))
                {
                    ok = _IsValidAddressNumber(str) ? ok : false;
                }
            }

            return ok;
        }

        public static bool _IsValidSubnetmaskBin(string addr)
        {
            bool ok = _IsValidAddressBin(addr);

            if (ok)
            {
                while (addr != _Reduce(addr))
                    addr = _Reduce(addr);
                ok = addr == "10" ? ok : false;
            }

            return ok;
        }

        public static bool _IsValidSubnetmaskInt(string addr)
        {
            return _IsValidAddressInt(addr) ? _IsValidSubnetmaskBin(Converters.AddressIntToBin(addr)) : false;
        }

        private static string _Reduce(string str)
        {
            return str.Replace("00", "0").Replace("11", "1").Replace(".", "");
        }

        private static bool _IsValidAddressNumber(string number)
        {
            try
            {
                int n = int.Parse(number);
                return (n <= 255 && n >= 0);
            }
            catch
            {
                return false;
            }
        }
    }
}
