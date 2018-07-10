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
        public NewNetPage()
        {
            this.InitializeComponent();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
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
                    else { isAddrOk = false; } break;
                case "txtIndirizzoBinario":
                    if (Validators.IsValidAddress(content, AddressType.NetworkAddress, AddressStructure.BinaryAddress))
                    {
                        txtIndirizzoStandard.Text = Converters.AddressBinToInt(content);
                    }
                    else { isAddrOk = false; } break;
                case "txtSubnetmaskStandard":
                    if(Validators.IsValidAddress(content, AddressType.SubnetmaskAddress, AddressStructure.IntegerAddress))
                    {
                        txtSubnetmaskBinario.Text = Converters.AddressIntToBin(content);
                        SetSlash(Converters.SubnetmaskToSlash(content));
                    }
                    else { isMaskOk = false; } break;
                case "txtSubnetmaskBinario":
                    if (Validators.IsValidAddress(content, AddressType.SubnetmaskAddress, AddressStructure.BinaryAddress))
                    {
                        txtSubnetmaskStandard.Text = Converters.AddressBinToInt(content);
                        SetSlash(Converters.SubnetmaskToSlash(content));
                    }
                    else { isMaskOk = false; } break;
            }
            //
            void SetSlash(int slash) { comboBoxSlash.SelectedIndex = slash - 1; }
        }

        private void ComboBoxSlash_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSubnetmaskStandard.Text = Converters.SubnetmaskSlashToInt(GetSlash());
            txtSubnetmaskBinario.Text = Converters.SubnetmaskSlashToBin(GetSlash());
            //
            int GetSlash() => int.Parse(((ComboBoxItem)comboBoxSlash.SelectedValue).Content.ToString().Substring(2));
        }

    }
}
