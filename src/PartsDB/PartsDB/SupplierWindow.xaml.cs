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
using System.Net;
using System.Diagnostics;
using PartsDB.Types;

namespace PartsDB
{
    /// <summary>
    /// Interaction logic for PartWindow.xaml
    /// </summary>
    public partial class SupplierWindow : Window
    {
        Helpers helper;
        Supplier SupplierDetails;
        public SupplierWindow(Supplier s, Helpers WindowHelper)
        {
            InitializeComponent();
            helper = WindowHelper;
            SupplierDetails = s;
            if (SupplierDetails != null)
            {
                SupplierNumberTextBox.Text = SupplierDetails.Number.ToString();
                SupplierNameTextBox.Text = SupplierDetails.Name;
                SupplierWebsiteTextBox.Text = SupplierDetails.Website;
                SupplierContactTextBox.Text = SupplierDetails.Contact;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SupplierDetails == null)
            {
                Task<(HttpStatusCode, Object)> CreateSupplierTask = helper.CreateSupplier(SupplierNameTextBox.Text, SupplierWebsiteTextBox.Text, SupplierContactTextBox.Text);
                dynamic response = await CreateSupplierTask;
                if (response.Item1 == HttpStatusCode.OK)
                {
                    MessageBox.Show("Successfully created supplier", "Supplier created", MessageBoxButton.OK, MessageBoxImage.Information);
                } else
                {
                    MessageBox.Show("Could not create supplier", "Supplier not created", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Task<(HttpStatusCode, Supplier)> UpdateSupplierTask = helper.UpdateSupplier(SupplierNumberTextBox.Text, SupplierNameTextBox.Text, SupplierWebsiteTextBox.Text, SupplierContactTextBox.Text);
                dynamic response = await UpdateSupplierTask;
                if (response.Item1 == HttpStatusCode.OK)
                {
                    MessageBox.Show("Successfully updated supplier", "Supplier updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Could not update supplier", "Supplier not updated", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Task<(HttpStatusCode, Object)> DeleteSupplierTask = helper.DeleteSupplier(SupplierNumberTextBox.Text);
            var response = await DeleteSupplierTask;
            if (response.Item1 == HttpStatusCode.NoContent)
            {
                MessageBox.Show("Successfully deleted supplier", "Supplier deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                Debug.WriteLine(response.Item2.ToString());
                MessageBox.Show("Could not delete supplier", "Supplier not deleted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
