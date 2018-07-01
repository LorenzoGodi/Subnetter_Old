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
        public Address(string addr)
        {
            if(Validators.IsValidAddress(addr))
            {

            }
        }

        //

        public static string ToStringInt()
        {

        }

        public static string ToStringBin()
        {

        }

        public static Address ConvertToAddress(string addr)
        {

        }
    }
}
