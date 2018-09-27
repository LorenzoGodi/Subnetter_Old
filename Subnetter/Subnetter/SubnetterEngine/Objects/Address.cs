using Subnetter.SubnetterEngine.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.SubnetterEngine.Objects
{
    class Address
    {
        //int startingSlash; // Subnetmask iniziale
        public List<string> AddressParts { private set; get; } // Parti dell'indirizzo binario
        List<Address> subnets; // Sottoreti create

        //

        public string AddressBin => Formatters.AddPoints(AddressParts.Aggregate((x, y) => x + y));
        public string AddressDec => Converters.AddressBinToInt(AddressBin);

        public int SubnetmaskSlash => 32 - AddressParts[0].Length;
        public string SubnetmaskBin => Converters.SubnetmaskSlashToBin(SubnetmaskSlash);
        public string SubnetmaskDec => Converters.SubnetmaskSlashToInt(SubnetmaskSlash);

        //

        public bool IsSubnetted => subnets != null;
        public bool IsMainAddress => AddressParts.Count == 1 && !IsSubnetted;

        public bool SubnetPossible => MaxSubnets > 0;
        public int MaxSubnets => AddressParts[AddressParts.Count - 1].Length - 1;

        //

        public Address(List<string> addressParts)
        {
            AddressParts = addressParts;
        }

        public Address(string address, int slash)
        {
            AddressParts = new List<string>();
            string add = Formatters.RemovePoints(Converters.AddressToBin(address));
            AddressParts.Add(add.Substring(0,slash)); // Parte subnetmask
            AddressParts.Add(add.Substring(slash)); // Parte Host
            //startingSlash = slash;


            if (!Validators.IsValidAddressNetwork(address, Converters.SubnetmaskSlashToBin(slash)))
                throw new Exception("Questo non è un indirizzo di rete");
        }

        public Address(string address, string subnetmask)
        {
            AddressParts = new List<string>();

            AddressParts.Add(Converters.AddressToBin(address));
            //startingSlash = Converters.SubnetmaskToSlash(subnetmask);


            if (!Validators.IsValidAddressNetwork(address, subnetmask))
                throw new Exception("Questo non è un indirizzo di rete");
        }

        //

        /// <summary>
        /// Procede alla creazione di sottoreti
        /// </summary>
        /// <param name="bits">Numero di bit della parte Host dell'attuale rete da utilizzare per creare sottoreti</param>
        public List<Address> MakeSubnets(int bits)
        {
            int tot_sottoreti = Convert.ToInt32(System.Math.Pow(2, bits));

            string rest = "";
            while(bits + rest.Length < AddressParts[AddressParts.Count - 1].Length) { rest += "0"; }

            if (tot_sottoreti <= MaxSubnets)
            {
                for (int v = 0; v < tot_sottoreti; v++)
                {
                    List<string> newSubnet = new List<string>(AddressParts);
                    newSubnet.RemoveAt(AddressParts.Count - 1);

                    string subnetPart = Convert.ToString(v, 2);
                    while (subnetPart.Length < bits) { subnetPart = "0" + subnetPart; }

                    newSubnet.Add(subnetPart);
                    newSubnet.Add(rest);
                    subnets.Add(new Address(newSubnet);
                }
                return subnets;
            }
            else
            {
                throw new Exception("Impossibile eseguire subnetting.");
            }
        }

        public void DeleteSubnets()
        {
            subnets.Clear();
        }
    }
}
