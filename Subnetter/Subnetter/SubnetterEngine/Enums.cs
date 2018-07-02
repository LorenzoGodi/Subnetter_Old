using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine
{
    public enum AddressType { NetworkAddress, SubnetmaskAddress }
    public enum AddressStructure { BinaryAddress, IntegerAddress }
    public enum AddressRole { Network, Host, Broadcast }
}
