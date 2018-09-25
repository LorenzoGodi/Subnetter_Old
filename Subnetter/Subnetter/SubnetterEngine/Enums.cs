using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine
{
    // Tipologia di indirizzo internet
    public enum AddressType { NetworkAddress, SubnetmaskAddress } // INDIRIZZO NORMALE  -  INDIRIZZO DI SUBNETMASK

    // Base dell'indirizzo
    public enum AddressStructure { BinaryAddress, IntegerAddress } // BINARIA  -  DECIMALE

    // Tipologia di indirizzo rispetto al range di indirizzi di una rete
    public enum AddressRole { Network, Host, Broadcast } // INDIRIZZO DI RETE (primo)  -  INDIRIZZO HOST (1 di n)  -  INDIRIZZO DI BROADCAST (ultimo)

    // Identificatore indirizzo di rete
    public enum NetworkAddressStatus { Valid, OnlyTwoHosts, NotValid} // INDIRIZZO VALIDO - INDIRIZZO CON SOLO UNO ZERO A DESTRA - INDIRIZZO TERMINANTE CON UN UNO
}
