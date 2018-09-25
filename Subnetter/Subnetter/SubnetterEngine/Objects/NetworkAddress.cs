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
        public int StartingSlash;
        public List<string> AddressParts;
        public List<NetworkAddress> Subnets;

        //

        public bool IsSubnetted => Subnets != null;
        public bool IsMainAddress => AddressParts.Count == 1 && !IsSubnetted;

        public int MaxSubnets => AddressParts[AddressParts.Count - 1].Length - 1;

        //

        public NetworkAddress(string address, int slash)
        {
            AddressParts = new List<string>();

            AddressParts.Add(Converters.AddressToBin(address));
            StartingSlash = slash;


            if (!Validators.IsValidAddressNetwork(address, Converters.SubnetmaskSlashToBin(slash)))
                throw new Exception("Questo non è un indirizzo di rete");
        }

        public NetworkAddress(string address, string subnetmask)
        {
            AddressParts = new List<string>();

            AddressParts.Add(Converters.AddressToBin(address));
            StartingSlash = Converters.SubnetmaskToSlash(subnetmask);


            if (!Validators.IsValidAddressNetwork(address, subnetmask))
                throw new Exception("Questo non è un indirizzo di rete");
        }

        //

        /// <summary>
        /// Procede alla creazione di sottoreti
        /// </summary>
        /// <param name="bits">Numero di bit della parte Host dell'attuale rete da utilizzare per creare sottoreti</param>
        public void MakeSubnets(int bits)
        {

        }
    }
}