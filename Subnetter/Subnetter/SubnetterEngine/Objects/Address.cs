using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subnetter.SubnetterEngine.Operators;

namespace Subnetter.SubnetterEngine.Objects
{
    class Address
    {
        public string BinaryAddress { get; private set; }
        public string BinarySubnetmask { get; private set; }
        public string IntegerAddress { get; private set; }
        public string IntegerSubnetmask { get; private set; }

        public AddressRole AddressRole { get; private set; }

        //

        public Address(string address, string subnetmask)
        {
            if (Validators.DetermineAddressStructure(address) == AddressStructure.IntegerAddress)
            {
                IntegerAddress = address;
                BinaryAddress = Converters.AddressIntToBin(address);
            }
            else
            {
                BinaryAddress = address;
                IntegerAddress = Converters.AddressBinToInt(address);
            }

            if (Validators.DetermineSubnetmaskStructure(subnetmask) == AddressStructure.IntegerAddress)
            {
                IntegerSubnetmask = subnetmask;
                BinarySubnetmask = Converters.AddressIntToBin(subnetmask);
            }
            else
            {
                BinarySubnetmask = subnetmask;
                IntegerSubnetmask = Converters.AddressBinToInt(subnetmask);
            }
        }
    }
}
