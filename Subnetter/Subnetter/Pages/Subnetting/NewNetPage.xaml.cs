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
using Subnetter.Classes.XamlObjects;
using Windows.UI.Xaml.Media.Animation;


using Microsoft.Toolkit.Uwp.UI.Helpers;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace Subnetter.Pages.Subnetting
{
    /// <summary>
    /// Pagina che permette di avviare un nuovo progetto di subnetting.
    /// </summary>
    public sealed partial class NewNetPage : Page
    {
        ObjectsAccessControl oac;

        bool addressStatus = false;
        bool subnetStatus = false;
        //public ThemeListener Listener { get; }
        //private void Current_ThemeChanged(object sender, Models.ThemeChangedArgs e)
        //{
        //    UpdateThemeState();
        //}
        //private void ThemeListenerPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    UpdateThemeState();
        //}
        //private void Listener_ThemeChanged(ThemeListener sender)
        //{
        //    UpdateThemeState();
        //}
        //private void UpdateThemeState()
        //{
        //    //SystemTheme.Text = Listener.CurrentThemeName;
        //    //CurrentTheme.Text = SampleController.Current.GetCurrentTheme().ToString();
        //}
        public NewNetPage()
        {
            this.RequestedTheme = ElementTheme.Dark;
            this.InitializeComponent();
            //Listener = new ThemeListener();
            //this.Loaded += ThemeListenerPage_Loaded;
            //Listener.ThemeChanged += Listener_ThemeChanged;
            //SampleController.Current.ThemeChanged += Current_ThemeChanged;

            //

            //Storyboard sb = this.Resources["Terremoto"] as Storyboard;
            //sb.Begin();

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
                addressStatus = false;
                //
                if (tag == "A1")
                {
                    if (Validators.IsValidAddress(content, AddressType.NetworkAddress, AddressStructure.IntegerAddress))
                    {
                        result = Converters.AddressIntToBin(content);
                        addressStatus = true;
                    }
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
                    {
                        result = Converters.AddressBinToInt(content);
                        addressStatus = true;
                    }
                    else
                        result = "";
                    if(txtIndirizzoStandard.Text != result)
                    {
                        oac.Block("A1");
                        txtIndirizzoStandard.Text = result;
                    }
                }
                SetErrorMessage();
            }
        }

        private void ChangedSubnet(object sender, TextChangedEventArgs e)
        {
            string tag = ((TextBox)sender).Tag.ToString();

            if (oac.IsFree(tag))
            {
                string content = ((TextBox)sender).Text;
                string result = "";
                subnetStatus = false;
                //
                if (tag == "B1")
                {
                    if (Validators.IsValidAddress(content, AddressType.SubnetmaskAddress, AddressStructure.IntegerAddress))
                    {
                        result = Converters.AddressIntToBin(content);
                        SetSlash(Converters.SubnetmaskToSlash(content));
                        subnetStatus = true;
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
                        subnetStatus = true;
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
                SetErrorMessage();
            }

            // F
            void SetSlash(int slash) { if (comboBoxSlash.SelectedIndex != slash) { oac.Block("B3"); comboBoxSlash.SelectedIndex = slash; } }
            void SetSlashEmpty() { SetSlash(0); }
        }

        private void ComboBoxSlash_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (oac.IsFree("B3"))
            {
                subnetStatus = GetComboValue() != "";
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
            SetErrorMessage();

            // F
            int GetSlash() => int.Parse(GetComboValue().Substring(2));
            string GetComboValue() => ((ComboBoxItem)comboBoxSlash.SelectedValue).Content.ToString();
        }

        private void SetErrorMessage()
        {
            string error = null;
            bool fail = false;
            try
            {
                fail = Validators.IsValidAddressNetwork(txtIndirizzoBinario.Text, txtSubnetmaskBinario.Text);
            }
            catch { }
            if (fail)
            {

            }
            else if (!addressStatus && !subnetStatus) { error = "Impostare un indirizzo di rete e una subnetmask validi"; }
            else if (!addressStatus) { error = "L'indirizzo di rete inserito non è valido"; }
            else if (!subnetStatus) { error = "La subnetmask inserita non è valida"; }
            else { error = "L'indirizzo di rete non rispetta la subnetmask inserita"; }

            //

            bool before = gridError2.Visibility == Visibility.Collapsed;
            if (error == null)
            {
                gridError2.Visibility = Visibility.Collapsed;
                if (button.Visibility == Visibility.Collapsed)
                {                    
                    button.Opacity = 0.1;
                    button.Visibility = Visibility.Visible;
                    Storyboard sb = this.Resources["ButtonShow"] as Storyboard;
                    sb.Begin();
                }
            }
            else
            {
                button.Visibility = Visibility.Collapsed;
                gridError2.Visibility = Visibility.Visible;
                //
                if(txtError.Text != error)
                {
                    txtError.Text = error;
                    Storyboard sb = this.Resources["Terremoto"] as Storyboard;
                    sb.Begin();
                }
                if (before)
                {
                    Storyboard sb = this.Resources["Terremoto"] as Storyboard;
                    sb.Begin();
                }
            }
        }
    }
}