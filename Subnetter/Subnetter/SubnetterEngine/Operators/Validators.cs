﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subnetter.SubnetterEngine.Objects;

namespace Subnetter.SubnetterEngine.Operators
{
    class Validators
    {
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

        public static AddressStructure DetermineAddressStructure(string address)
        {
            try
            {
                if (_IsValidAddressInt(address))
                    return AddressStructure.IntegerAddress;
                else if (_IsValidAddressBin(address))
                    return AddressStructure.BinaryAddress;
                else
                    throw null;
            }
            catch
            {
                throw new Exception("Indirizzo non valido");
            }
        }

        public static AddressStructure DetermineSubnetmaskStructure(string address)
        {
            try
            {
                if (_IsValidSubnetmaskInt(address))
                    return AddressStructure.IntegerAddress;
                else if (_IsValidSubnetmaskBin(address))
                    return AddressStructure.BinaryAddress;
                else
                    throw null;
            }
            catch
            {
                throw new Exception("Indirizzo di subnetmask non valido");
            }
        }

        //

        private static bool _IsValidAddressBin(string addr)
        {
            string valid = "10.";
            bool ok = true;

            if (ok = (!string.IsNullOrEmpty(addr)) && addr.Contains(".") && addr.Split('.').Length == 4)
            {
                foreach (char chr in addr)
                    ok = valid.Contains(chr) ? ok : false;

                foreach (string str in addr.Split('.'))
                {
                    ok = str.Length == 8 ? ok : false;
                }

                foreach (string str in addr.Split('.'))
                {
                    ok = _IsValidAddressNumber(Convert.ToInt32(str, 2).ToString()) ? ok : false;
                }
            }

            return ok;
        }

        private static bool _IsValidAddressInt(string addr)
        {
            string valid = "1234567890.";
            bool ok = true;

            if (ok = (!string.IsNullOrEmpty(addr)) && addr.Contains(".") && addr.Split('.').Length == 4)
            {
                foreach (char chr in addr)
                    ok = valid.Contains(chr) ? ok : false;

                foreach (string str in addr.Split('.'))
                {
                    ok = _IsValidAddressNumber(str) ? ok : false;
                }
            }

            return ok;
        }

        private static bool _IsValidSubnetmaskBin(string addr)
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

        private static bool _IsValidSubnetmaskInt(string addr)
        {
            return _IsValidAddressInt(addr) ? _IsValidSubnetmaskBin(Converters.AddressIntToBin(addr)) : false;
        }

        private static string _Reduce(string str)
        {
            return str.Replace("00", "0").Replace("11", "1").Replace(".", "");
        }

        private static bool _IsValidAddressNumber(string number)
        {
            for (int v = 0; v <= 255; v++)
                if (number == v.ToString())
                    return true;
            return false;
        }
    }
}
