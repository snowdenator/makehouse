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
using System.Diagnostics;
using System.Net;
using PartsDB.Types;

namespace PartsDB
{
    /// <summary>
    /// Interaction logic for FinderWindow.xaml
    /// </summary>
    public partial class FinderWindow : Window
    {
        Helpers helper = new Helpers();
        string session;

        public FinderWindow(string session_id)
        {
            InitializeComponent();
            session = session_id;
            SessionLabel.Text = "Session " + session;
            helper.Initialise(session);
            UpdateDataGrids();
        }

        private async void UpdateDataGrids()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            List<Part> PartsData = await helper.GetParts();
            PartsDataGrid.ItemsSource = PartsData;
            

            List<Supplier> SupplierData = await helper.GetSuppliers();
            SuppliersDataGrid.ItemsSource = SupplierData;
            Mouse.OverrideCursor = null;
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Task<(HttpStatusCode, Object)> LogoutTask = helper.Logout();
            dynamic Response = await LogoutTask;
            if (Response.Item1 == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Successfully destroyed session, exiting");
                Application.Current.Shutdown();
            }
            else
            {
                Debug.WriteLine("Error destroying session! Exiting anyway");
                Application.Current.Shutdown();
            }
        }

        private void RefreshInfoButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrids();
        }

        private void SuppliersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Supplier CurrentRow = (Supplier)SuppliersDataGrid.CurrentItem;
            SupplierWindow supplier = new SupplierWindow(CurrentRow, helper);
            supplier.Owner = this;
            supplier.Show();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Task<(HttpStatusCode, Object)> LogoutTask = helper.Logout();
            dynamic Response = await LogoutTask;
            if (Response.Item1 == HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Successfully destroyed session, exiting");
                Application.Current.Shutdown();
            }
            else
            {
                Debug.WriteLine("Error destroying session! Exiting anyway");
                Application.Current.Shutdown();
            }
        }

        private void PartsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Part CurrentRow = (Part)PartsDataGrid.CurrentItem;
            PartWindow part = new PartWindow(CurrentRow, helper);
            part.Owner = this;
            part.Show();
        }

        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            SupplierWindow supplier = new SupplierWindow(null, helper);
            supplier.Owner = this;
            supplier.Show();
        }

        private void AddPartButton_Click(object sender, RoutedEventArgs e)
        {
            PartWindow part = new PartWindow(null, helper);
            part.Owner = this;
            part.Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Owner = this;
            settings.Show();
        }

        private void PartsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            /*
             * This is to fix the titles that gets displayed within the datagrid so it looks less ugly
             * Although, this method feels ugly...
             */

            String ColumnHeaderName = e.Column.Header.ToString();

            switch (ColumnHeaderName)
            {
                case "ManufacturerPartNumber":
                    e.Column.Header = "Manufacturer P/N";
                    break;
                case "StockLevel":
                    e.Column.Header = "Stock Level";
                    break;
                case "StockNumber":
                    e.Column.Header = "Stock Number";
                    break;
                case "PreferredSupplier":
                    e.Column.Header = "Preferred Supplier";
                    break;
                case "Supplier1":
                    e.Column.Header = "Supplier 1";
                    break;
                case "Supplier1MinimumQty":
                    e.Column.Header = "Supp. 1 Minimum Qty.";
                    break;
                case "Supplier1PartNumber":
                    e.Column.Header = "Supp. 1 P/N";
                    break;
                case "Supplier1Price":
                    e.Column.Header = "Supp. 1 Price";
                    break;
                case "Supplier2":
                    e.Column.Header = "Supplier 2";
                    break;
                case "Supplier2MinimumQty":
                    e.Column.Header = "Supp. 2 Minimum Qty.";
                    break;
                case "Supplier2PartNumber":
                    e.Column.Header = "Supp. 2 P/N";
                    break;
                case "Supplier2Price":
                    e.Column.Header = "Supp. 2 Price";
                    break;
                default:
                    break;
            }
        }
    }
}
