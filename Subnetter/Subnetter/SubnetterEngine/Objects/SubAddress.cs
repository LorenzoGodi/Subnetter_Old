using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subnetter.SubnetterEngine.Operators;

namespace Subnetter.SubnetterEngine.Objects
{
    class SubAddress
    {
        private string addressHead;
        private List<string> subnetsParts;

        //

        public string BinarySubnetmask
        {
            get
            {
                string result = "";
                while(result.Length < addressHead.Length + SubnetLength)
                    result += "1";
                result = Formatters.CompleteAddressHead(result, '0');
                result = Formatters.AddPoints(result);
                return result;
            }
        }
        public string BinaryAddress
        {
            get
            {
                string result = addressHead + Formatters.Merge(subnetsParts, "");
                result = Formatters.CompleteAddressHead(result, '0');
                result = Formatters.AddPoints(result);
                return result;
            }
        }
        public string IntegerSubnetmask
        {
            get { return Converters.AddressBinToInt(BinarySubnetmask); }
        }
        public string IntegerAddress
        {
            get { return Converters.AddressBinToInt(BinaryAddress); }
        }

        public string BinaryNetworkPortion
        {
            get { return addressHead + Formatters.Merge(subnetsParts, ""); }
        }

        public string BinaryHostPortion
        {
            get
            {
                string result = "";
                while (result.Length < 32 - BinaryNetworkPortion.Length)
                    result += "0";
                return result;
            }
        }

        public int SubnetLength
        {
            get { return Formatters.Merge(subnetsParts, "").Length; }
        }

        public int MaxBitsOfMoreSubnet
        {
            get { return 32 - (addressHead.Length + SubnetLength + 2); }
        }

        //

        public SubAddress(string addressHead, List<string> existingSubnets, int newSubnetBits)
        {
            this.addressHead = addressHead;
            subnetsParts = new List<string>(existingSubnets);

            if (!Validators.IsValidAddressNetwork(IntegerAddress, IntegerSubnetmask))
                throw new Exception("Questo non è un indirizzo di rete");

            //


        }        
    }
}