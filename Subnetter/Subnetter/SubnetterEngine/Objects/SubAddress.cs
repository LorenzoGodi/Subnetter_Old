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
        private string[] subnets;

        /*

        public string BinarySubnetmask
        {
            get
            {
                return null;
            }
        }
        public string BinaryAddress
        {
            get
            {
                return null;
            }
        }
        public string IntegerSubnetmask
        {
            get
            {
                return null;
            }
        }
        public string IntegerAddress
        {
            get
            {
                return null;
            }
        }

        */

        public SubAddress(int newSubnetBits, string addressHead, params string[] existingSubnets)
        {

        }
    }
}