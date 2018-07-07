using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subnetter.SubnetterEngine.Operators;

namespace Subnetter.SubnetterEngine.Objects
{
    class NetworkAddress
    {
        public string BinarySubnetmask { get; private set; }
        public string IntegerSubnetmask { get; private set; }

        public string BinaryAddress { get; private set; }
        public string IntegerAddress { get; private set; }

        public List<SubAddress> Subnets { get; private set; }

        //

        public NetworkAddress(string address, string subnetmask)
        {
            if (AI.DetermineAddressStructure(address) == AddressStructure.IntegerAddress)
            {
                IntegerAddress = address;
                BinaryAddress = Converters.AddressIntToBin(address);
            }
            else
            {
                BinaryAddress = address;
                IntegerAddress = Converters.AddressBinToInt(address);
            }

            if (AI.DetermineSubnetmaskStructure(subnetmask) == AddressStructure.IntegerAddress)
            {
                IntegerSubnetmask = subnetmask;
                BinarySubnetmask = Converters.AddressIntToBin(subnetmask);
            }
            else
            {
                BinarySubnetmask = subnetmask;
                IntegerSubnetmask = Converters.AddressBinToInt(subnetmask);
            }

            if (!Validators.IsValidAddressNetwork(address, subnetmask))
                throw new Exception("Questo non è un indirizzo di rete");
        }
    }
}