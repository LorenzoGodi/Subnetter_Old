using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Operators
{
    class AI
    {
        public static AddressStructure DetermineAddressStructure(string address)
        {
            try
            {
                if (Validators._IsValidAddressInt(address))
                    return AddressStructure.IntegerAddress;
                else if (Validators._IsValidAddressBin(address))
                    return AddressStructure.BinaryAddress;
                else
                    throw new Exception("Indirizzo non valido");
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
                if (Validators._IsValidSubnetmaskInt(address))
                    return AddressStructure.IntegerAddress;
                else if (Validators._IsValidSubnetmaskBin(address))
                    return AddressStructure.BinaryAddress;
                else
                    throw null;
            }
            catch
            {
                throw new Exception("Indirizzo di subnetmask non valido");
            }
        }
    }
}
