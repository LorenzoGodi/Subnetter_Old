using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Subnetter.SubnetterEngine;
using Subnetter.SubnetterEngine.Operators;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace Subnetter.Pages.Subnetting
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class NewNetPage : Page
    {
        ObjectsAccessControl oac;

        public NewNetPage()
        {
            this.InitializeComponent();

            //

            List<object> objs = new List<object>();
            objs.Add(txtSubnetmaskStandard);
            objs.Add(txtSubnetmaskBinario);
            objs.Add(comboBoxSlash);

            //
            
            oac = new ObjectsAccessControl();
            oac.AddObj("A1", "A2", "B1", "B2", "B3"); // = Tags

            //
            comboBoxSlash.SelectedIndex = 0;
        }


        private void ChangedAddress(object sender, TextChangedEventArgs e)
        {
            string tag = ((TextBox)sender).Tag.ToString();

            if (oac.IsFree(tag))
            {
                string content = ((TextBox)sender).Text;
                string result = "";
                //
                if (tag == "A1")
                {
                    if (Validators.IsValidAddress(content, AddressType.NetworkAddress, AddressStructure.IntegerAddress))
                        result = Converters.AddressIntToBin(content);
                    else
                        result = "";
                    if(txtIndirizzoBinario.Text != result)
                    {
                        oac.Block("A2");
                        txtIndirizzoBinario.Text = result;
                    }
                }
                else
                {
                    if (Validators.IsValidAddress(content, AddressType.NetworkAddress, AddressStructure.BinaryAddress))
                        result = Converters.AddressBinToInt(content);
                    else
                        result = "";
                    if(txtIndirizzoStandard.Text != result)
                    {
                        oac.Block("A1");
                        txtIndirizzoStandard.Text = result;
                    }
                }
            }
        }

        private void ChangedSubnet(object sender, TextChangedEventArgs e)
        {
            string tag = ((TextBox)sender).Tag.ToString();

            if (oac.IsFree(tag))
            {
                string content = ((TextBox)sender).Text;
                string result = "";
                //
                if (tag == "B1")
                {
                    if (Validators.IsValidAddress(content, AddressType.SubnetmaskAddress, AddressStructure.IntegerAddress))
                    {
                        result = Converters.AddressIntToBin(content);
                        SetSlash(Converters.SubnetmaskToSlash(content));
                    }
                    else
                    {
                        result = "";
                        SetSlashEmpty();
                    }
                    if (txtSubnetmaskBinario.Text != result)
                    {
                        oac.Block("B2");
                        txtSubnetmaskBinario.Text = result;
                    }
                }
                else
                {
                    if (Validators.IsValidAddress(content, AddressType.SubnetmaskAddress, AddressStructure.BinaryAddress))
                    {
                        result = Converters.AddressBinToInt(content);
                        SetSlash(Converters.SubnetmaskToSlash(content));
                    }
                    else
                    {
                        result = "";
                        SetSlashEmpty();
                    }
                    if (txtSubnetmaskStandard.Text != result)
                    {
                        oac.Block("B1");
                        txtSubnetmaskStandard.Text = result;
                    }
                }
                
            }

            // F
            void SetSlash(int slash) { if (comboBoxSlash.SelectedIndex != slash) { oac.Block("B3"); comboBoxSlash.SelectedIndex = slash; } }
            void SetSlashEmpty() { SetSlash(0); }
        }

        private void ComboBoxSlash_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (oac.IsFree("B3"))
            {
                string result1 = GetComboValue() != "" ? Converters.SubnetmaskSlashToInt(GetSlash()) : "";
                string result2 = GetComboValue() != "" ? Converters.SubnetmaskSlashToBin(GetSlash()) : "";

                if (txtSubnetmaskStandard.Text != result1)
                {
                    txtSubnetmaskStandard.Text = result1;
                    oac.Block("B1");
                }

                if (txtSubnetmaskBinario.Text != result2)
                {
                    txtSubnetmaskBinario.Text = result2;
                    oac.Block("B2");
                }
            }

            // F
            int GetSlash() => int.Parse(GetComboValue().Substring(2));
            string GetComboValue() => ((ComboBoxItem)comboBoxSlash.SelectedValue).Content.ToString();
        }

        class ObjectsAccessControl
        {
            class Obj
            {
                public string name;
                public int blockTimes;

                public Obj(string name)
                {
                    this.name = name;
                    blockTimes = 0;
                }
            }

            List<Obj> objects;

            public ObjectsAccessControl()
            {
                objects = new List<Obj>();
            }

            public void AddObj(string name)
            {
                objects.Add(new Obj(name));
            }

            public void AddObj(params string[] names)
            {
                foreach(string name in names)
                    objects.Add(new Obj(name));
            }

            public void Block(string name)
            {
                for (int v = 0; v < objects.Count; v++)
                    if (objects[v].name == name)
                        objects[v].blockTimes++;
            }

            public bool IsFree(string name)
            {
                for (int v = 0; v < objects.Count; v++)
                {
                    if (objects[v].name == name)
                    {
                        if(objects[v].blockTimes > 0) { objects[v].blockTimes--; return false; }
                        return true;
                    }
                }
                throw new Exception("Oggetto non contenuto nella lista");
            }
        }
    }
}
