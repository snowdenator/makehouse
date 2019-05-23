using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PartsDB.Types;
using System.Net;
using System.Diagnostics;

namespace PartsDB
{
    /// <summary>
    /// Interaction logic for PartWindow.xaml
    /// </summary>
    public partial class PartWindow : Window
    {
        Helpers helper;
        Part PartDetails = null;
        public PartWindow(Part p, Helpers WindowHelper)
        {
            InitializeComponent();
            helper = WindowHelper;
            PartDetails = p;
            if (PartDetails != null)
            {
                StockNumberTextBox.Text = PartDetails.StockNumber.ToString();
                StockLevelUpDown.Value = PartDetails.StockLevel;
                DescriptionTextBox.Text = PartDetails.Description;
                ManufacturerTextBox.Text = PartDetails.Manufacturer;
                MpnTextBox.Text = PartDetails.ManufacturerPartNumber;
                Supplier1PriceTextBox.Text = PartDetails.Supplier1Price.ToString();
                Supplier1PnTextBox.Text = PartDetails.Supplier1PartNumber;
                Supplier1MinQtyUpDwn.Value = PartDetails.Supplier1MinimumQty;
                Supplier2PnTextBox.Text = PartDetails.Supplier2PartNumber;
                Supplier2PriceTextBox.Text = PartDetails.Supplier2Price.ToString();
                Supplier2MinQtyUpDwn.Value = PartDetails.Supplier2MinimumQty;

                PopulateSuppliers();
                PrefSupplierComboBox.SelectedItem = PartDetails.PreferredSupplier.Name;
                Supplier1ComboBox.SelectedItem = PartDetails.Supplier1.Name;
                Supplier2ComboBox.SelectedItem = PartDetails.Supplier2.Name;

            } else
            {
                PopulateSuppliers();
            }
        }

        private async void PopulateSuppliers()
        {
            List<Supplier> SupplierData = await helper.GetSuppliers();
            foreach (Supplier s in SupplierData)
            {
                PrefSupplierComboBox.Items.Add(s.Name);
                Supplier1ComboBox.Items.Add(s.Name);
                Supplier2ComboBox.Items.Add(s.Name);

            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (PartDetails == null)
            {
                List<Supplier> SupplierList = await helper.GetSuppliers();
                Supplier PreferredSupplier = SupplierList.Find(s => s.Name == PrefSupplierComboBox.SelectedItem.ToString());
                Supplier Supplier1 = SupplierList.Find(s => s.Name == Supplier1ComboBox.SelectedItem.ToString());
                Supplier Supplier2 = SupplierList.Find(s => s.Name == Supplier2ComboBox.SelectedItem.ToString());

                dynamic Response = await helper.CreatePart(DescriptionTextBox.Text,
                    ManufacturerTextBox.Text,
                    MpnTextBox.Text,
                    PreferredSupplier.Number,
                    StockLevelUpDown.Value.Value,
                    Supplier1.Number,
                    Supplier1MinQtyUpDwn.Value.Value,
                    Convert.ToDecimal(Supplier1PriceTextBox.Text),
                    Supplier1PnTextBox.Text,
                    Supplier2.Number,
                    Supplier2MinQtyUpDwn.Value.Value,
                    Convert.ToDecimal(Supplier2PriceTextBox.Text),
                    Supplier2PnTextBox.Text);

                if (Response.Item1 == HttpStatusCode.OK)
                {
                    MessageBox.Show("Successfully created part", "Part created", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else
            {
                List<Supplier> SupplierList = await helper.GetSuppliers();
                Supplier PreferredSupplier = SupplierList.Find(s => s.Name == PrefSupplierComboBox.SelectedItem.ToString());
                Supplier Supplier1 = SupplierList.Find(s => s.Name == Supplier1ComboBox.SelectedItem.ToString());
                Supplier Supplier2 = SupplierList.Find(s => s.Name == Supplier2ComboBox.SelectedItem.ToString());

                dynamic Response = await helper.UpdatePart(Convert.ToInt32(StockNumberTextBox.Text),
                    DescriptionTextBox.Text,
                    ManufacturerTextBox.Text,
                    MpnTextBox.Text,
                    PreferredSupplier.Number,
                    StockLevelUpDown.Value.Value,
                    Supplier1.Number,
                    Supplier1MinQtyUpDwn.Value.Value,
                    Convert.ToDecimal(Supplier1PriceTextBox.Text),
                    Supplier1PnTextBox.Text,
                    Supplier2.Number,
                    Supplier2MinQtyUpDwn.Value.Value,
                    Convert.ToDecimal(Supplier2PriceTextBox.Text),
                    Supplier2PnTextBox.Text);

                if (Response.Item1 == HttpStatusCode.OK)
                {
                    MessageBox.Show("Successfully updated part", "Part updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Task<(HttpStatusCode, Object)> DeletePartTask = helper.DeletePart(Convert.ToInt32(StockNumberTextBox.Text));
            var response = await DeletePartTask;
            if (response.Item1 == HttpStatusCode.NoContent)
            {
                MessageBox.Show("Successfully deleted part", "Part deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                Debug.WriteLine(response.Item2.ToString());
                MessageBox.Show("Could not delete part", "Part not deleted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            if (PartDetails != null)
            {
                helper.PrinterInitialise();
                helper.PrintLabel(MpnTextBox.Text, DescriptionTextBox.Text, ManufacturerTextBox.Text, StockNumberTextBox.Text);
            }
        }
    }
    
}
