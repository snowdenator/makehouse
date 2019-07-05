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
using System.Configuration;

namespace PartsDB
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            ServerTextBox.Text = ConfigurationManager.AppSettings["server_address"];
            DomainTextBox.Text = ConfigurationManager.AppSettings["domain"];
            PrinterPortTextBox.Text = ConfigurationManager.AppSettings["printer_port"];
            PrinterBaudTextBox.Text = ConfigurationManager.AppSettings["printer_baud"];
            PrinterEnabledCheckBox.IsChecked = Convert.ToBoolean(ConfigurationManager.AppSettings["printer_enabled"]);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        { 
            ConfigurationManager.AppSettings["server_address"] = ServerTextBox.Text;
            ConfigurationManager.AppSettings["domain"] = DomainTextBox.Text;
            ConfigurationManager.AppSettings["printer_port"] = PrinterPortTextBox.Text;
            ConfigurationManager.AppSettings["printer_baud"] = PrinterBaudTextBox.Text;
            ConfigurationManager.AppSettings["printer_enabled"] = PrinterEnabledCheckBox.IsChecked.ToString();
        }
    }
}
