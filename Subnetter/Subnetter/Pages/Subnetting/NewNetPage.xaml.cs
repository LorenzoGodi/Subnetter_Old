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
        ModifyControl mc;

        public NewNetPage()
        {
            this.InitializeComponent();

            //

            List<object> objs = new List<object>();
            objs.Add(txtSubnetmaskStandard);
            objs.Add(txtSubnetmaskBinario);
            objs.Add(comboBoxSlash);

            mc = new ModifyControl(objs);

            //
            comboBoxSlash.SelectedIndex = 0;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            bool a = mc.Access(sender);
            if (a)
            {
                string content = ((TextBox)sender).Text;
                //
                bool isMaskOk = true;
                bool isAddrOk = true;

                switch (((TextBox)sender).Name)
                {
                    case "txtIndirizzoStandard":
                        if (Validators.IsValidAddress(content, AddressType.NetworkAddress, AddressStructure.IntegerAddress))
                        {
                            txtIndirizzoStandard.Text = Converters.AddressIntToBin(content);
                        }
                        else
                        {
                            isAddrOk = false;
                        }
                        break;
                    case "txtIndirizzoBinario":
                        if (Validators.IsValidAddress(content, AddressType.NetworkAddress, AddressStructure.BinaryAddress))
                        {
                            txtIndirizzoStandard.Text = Converters.AddressBinToInt(content);
                        }
                        else { isAddrOk = false; }
                        break;
                    case "txtSubnetmaskStandard":
                        if (Validators.IsValidAddress(content, AddressType.SubnetmaskAddress, AddressStructure.IntegerAddress))
                        {
                            if (txtSubnetmaskBinario.Text != Converters.AddressIntToBin(content)) { txtSubnetmaskBinario.Text = Converters.AddressIntToBin(content); } else { mc.Free(txtSubnetmaskBinario); }
                            if(GetSlash() != Converters.SubnetmaskToSlash(content)) { SetSlash(Converters.SubnetmaskToSlash(content)); } else { mc.Free(comboBoxSlash); }
                        } 
                        else
                        {
                            isMaskOk = false;
                            if (txtSubnetmaskBinario.Text != "") { txtSubnetmaskBinario.Text = ""; } else { mc.Free(txtSubnetmaskBinario); }
                            if (GetComboValue() != "") { SetSlashEmpty(); } else { mc.Free(comboBoxSlash); }
                        }
                        break;
                    case "txtSubnetmaskBinario":
                        if (Validators.IsValidAddress(content, AddressType.SubnetmaskAddress, AddressStructure.BinaryAddress))
                        {
                            if (txtSubnetmaskStandard.Text != Converters.AddressBinToInt(content)) { txtSubnetmaskStandard.Text = Converters.AddressBinToInt(content); } else { mc.Free(txtSubnetmaskStandard); }
                            if (GetSlash() != Converters.SubnetmaskToSlash(content)) { SetSlash(Converters.SubnetmaskToSlash(content)); } else { mc.Free(comboBoxSlash); }
                        }
                        else
                        {
                            isMaskOk = false;
                            if (txtSubnetmaskStandard.Text != "") { txtSubnetmaskStandard.Text = ""; } else { mc.Free(txtSubnetmaskStandard); }
                            if (GetComboValue() != "") { SetSlashEmpty(); } else { mc.Free(comboBoxSlash); }
                        }
                        break;
                }
                // F
                void SetSlash(int slash) { comboBoxSlash.SelectedIndex = slash; }
                void SetSlashEmpty() { comboBoxSlash.SelectedIndex = 0; }
                int GetSlash() => (GetComboValue() != "") ? int.Parse(GetComboValue().Substring(2)) : -1;
                string GetComboValue() => ((ComboBoxItem)comboBoxSlash.SelectedValue).Content.ToString();
                void FreeCombo() { mc.Free(comboBoxSlash); }
            }
        }

        private void ComboBoxSlash_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool a = mc.Access(sender);
            if (a)
            {
                if(GetComboValue() != "")
                {
                    txtSubnetmaskStandard.Text = Converters.SubnetmaskSlashToInt(GetSlash());
                    txtSubnetmaskBinario.Text = Converters.SubnetmaskSlashToBin(GetSlash());
                }
                else
                {
                    if (txtSubnetmaskBinario.Text != "") { txtSubnetmaskBinario.Text = ""; } else { mc.Free(txtSubnetmaskBinario); }
                    if (txtSubnetmaskStandard.Text != "") { txtSubnetmaskStandard.Text = ""; } else { mc.Free(txtSubnetmaskStandard); }
                }
                //
                int GetSlash() => int.Parse(GetComboValue().Substring(2));
                string GetComboValue() => ((ComboBoxItem)comboBoxSlash.SelectedValue).Content.ToString();
            }
        }

        class ModifyControl
        {
            List<object> freeObjects;
            List<object> blockedObjects;

            //

            public ModifyControl(List<object> objects)
            {
                freeObjects = new List<object>(objects);
                blockedObjects = new List<object>();
            }

            public bool Access(object sender)
            {
                if(blockedObjects.Contains(sender))
                {
                    freeObjects.Add(sender);
                    blockedObjects.Remove(sender);
                    //
                    return false;
                }
                else
                {
                    if(blockedObjects.Count > 0)
                    {
                        throw new Exception("Errore :/");
                    }
                    //
                    foreach(object obj in freeObjects)
                    {
                        if(obj != sender)
                        {
                            blockedObjects.Add(obj);
                        }
                    }
                    freeObjects = new List<object>();
                    freeObjects.Add(sender);
                    //
                    return true;
                }
            }

            public void Free(object sender)
            {
                if (blockedObjects.Contains(sender))
                {
                    freeObjects.Add(sender);
                    blockedObjects.Remove(sender);
                }
            }

            public void FreeAll(object sender)
            {
                foreach (object obj in freeObjects)
                    freeObjects.Add(obj);
                blockedObjects = new List<object>();
            }
        }

    }
}
