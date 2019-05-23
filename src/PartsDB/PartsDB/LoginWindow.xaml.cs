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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;


namespace PartsDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        string session;
        public LoginWindow()
        {
            InitializeComponent();
            ServerTextBox.Text = ConfigurationManager.AppSettings["server_address"];
            string domain = ConfigurationManager.AppSettings["domain"];
        }

        private async Task<(HttpStatusCode, Boolean)> TryLogin(String username, String password)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string domain = ConfigurationManager.AppSettings["domain"];
                    var loginDetails = Encoding.ASCII.GetBytes(username + "@" + domain + ":" + password);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(loginDetails));
                    HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["server_address"] + "sessions", null);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        dynamic sessionResponse = JsonConvert.DeserializeObject(responseBody);

                        string sessionID = sessionResponse.session;

                        Debug.WriteLine("Session ID: " + sessionID);

                        session = sessionID;

                        return (response.StatusCode, true);
                    }
                    else
                    {
                        return (response.StatusCode, false);
                    }
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("HTTP Exception: {0}", e.Message);
                    return (0, false);
                }
            }
        }

        private async void LoginButton_Click_1(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Task<(HttpStatusCode, Boolean)> LoginTask = TryLogin(UsernameTextBox.Text, PasswordTextBox.Password);
            var loggedIn = await LoginTask;

            if (loggedIn.Item2 == true)
            {
                Debug.WriteLine("Logged in successfully");
                this.Hide();
                FinderWindow finder = new FinderWindow(session);
                finder.Show();
            } else if (loggedIn.Item1 == HttpStatusCode.Forbidden)
            {
                MessageBox.Show("Invalid login credentials!", "Failed login", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            Mouse.OverrideCursor = null;
        }
    }
}
