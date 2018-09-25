using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Operators
{
    class AI
    {
        /// <summary>
        /// Determina in quale base è la stringa di indirizzo, binaria o decimale
        /// </summary>
        /// <param name="address">Stringa dell'indirizzo</param>
        /// <returns>Enum che indica il tipo di base</returns>
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


        /// <summary>
        /// Determina in quale base è la stringa di indirizzo di subnetmask, binaria o decimale
        /// </summary>
        /// <param name="address">Stringa dell'indirizzo di subnetmask</param>
        /// <returns>Enum che indica il tipo di base</returns>
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
