using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;
using PartsDB.Types;
using System.IO;
using SATOPrinterAPI;

namespace PartsDB
{
    public class Helpers
    {
        private HttpClient client = new HttpClient();
        private string session;
        private Printer LabelPrinter = new Printer();
        public Boolean PrinterEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["printer_enabled"]);

        public void Initialise(string session_id)
        {
            // Setup basic parameters for httpclient
            client.DefaultRequestHeaders.Add("X-Session-ID", session_id);
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["server_address"]);
            session = session_id;
        }
        public async Task<(HttpStatusCode, Object)> Logout()
        {
            // Destroys a session
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("sessions");
                Debug.WriteLine("Destroyed session " + session);
                return (response.StatusCode, null);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return (0, null);
            }
        }

        public async Task<(HttpStatusCode, Object)> CreateSupplier(String SupplierName, String SupplierWebsite, String SupplierContact)
        {
            HttpContent data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("supplier_name", SupplierName),
                new KeyValuePair<string, string>("supplier_website", SupplierWebsite),
                new KeyValuePair<string, string>("supplier_contact", SupplierContact)
            });

            try
            {
                HttpResponseMessage response = await client.PostAsync("suppliers", data);
                Object ResponseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                return (response.StatusCode, ResponseBody);
            }  catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return (0, null);
            }
        }

        public async Task<(HttpStatusCode, Supplier)> UpdateSupplier(String SupplierNumber, String SupplierName, String SupplierWebsite, String SupplierContact)
        {
            HttpContent data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("supplier_name", SupplierName),
                new KeyValuePair<string, string>("supplier_website", SupplierWebsite),
                new KeyValuePair<string, string>("supplier_contact", SupplierContact)
            });

            try
            {
                HttpResponseMessage response = await client.PutAsync("suppliers/" + SupplierNumber, data);
                Supplier ResponseBody = JsonConvert.DeserializeObject<Supplier>(await response.Content.ReadAsStringAsync());
                return (response.StatusCode, ResponseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return (0, null);
            }
        }

        public async Task<(HttpStatusCode, Object)> DeleteSupplier(String SupplierNumber)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("suppliers/" + SupplierNumber);
                Object ResponseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                return (response.StatusCode, ResponseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return (0, null);
            }
        }

        public async Task<(HttpStatusCode, Object)> CreatePart(String Description, String Manufacturer, String MfgPartNumber, int PreferredSupplier, int StockLevel, int Supplier1, int Supplier1MinimumQty, decimal Supplier1Price, String Supplier1PartNumber, int Supplier2, int Supplier2MinimumQty, decimal Supplier2Price, String Supplier2PartNumber)
        {
            // TODO: Try make this use the Part type rather than a bunch of strings
            try
            {
                HttpContent PartData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<String, String>("description", Description),
                    new KeyValuePair<String, String>("manufacturer", Manufacturer),
                    new KeyValuePair<String, String>("mfg_partnumber", MfgPartNumber),
                    new KeyValuePair<String, String>("preferred_supplier", PreferredSupplier.ToString()),
                    new KeyValuePair<String, String>("stock_level", StockLevel.ToString()),
                    new KeyValuePair<String, String>("supplier_1", Supplier1.ToString()),
                    new KeyValuePair<String, String>("supplier_1_min_qty", Supplier1MinimumQty.ToString()),
                    new KeyValuePair<String, String>("supplier_1_price", Supplier1Price.ToString()),
                    new KeyValuePair<String, String>("supplier_1_partnumber", Supplier1PartNumber),
                    new KeyValuePair<String, String>("supplier_2", Supplier2.ToString()),
                    new KeyValuePair<String, String>("supplier_2_min_qty", Supplier2MinimumQty.ToString()),
                    new KeyValuePair<String, String>("supplier_2_price", Supplier2Price.ToString()),
                    new KeyValuePair<String, String>("supplier_2_partnumber", Supplier2PartNumber),
                });

                HttpResponseMessage Response = await client.PostAsync("parts", PartData);

                Object ResponseBody = JsonConvert.DeserializeObject(await Response.Content.ReadAsStringAsync());

                return (Response.StatusCode, ResponseBody);
            } catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return (0, null);
            }
        }

        public async Task<(HttpStatusCode, Object)> UpdatePart(int StockNumber, String Description, String Manufacturer, String MfgPartNumber, int PreferredSupplier, int StockLevel, int Supplier1, int Supplier1MinimumQty, decimal Supplier1Price, String Supplier1PartNumber, int Supplier2, int Supplier2MinimumQty, decimal Supplier2Price, String Supplier2PartNumber)
        {
            // TODO: Try make this use the Part type rather than a bunch of strings
            try
            {
                HttpContent PartData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<String, String>("description", Description),
                    new KeyValuePair<String, String>("manufacturer", Manufacturer),
                    new KeyValuePair<String, String>("mfg_partnumber", MfgPartNumber),
                    new KeyValuePair<String, String>("preferred_supplier", PreferredSupplier.ToString()),
                    new KeyValuePair<String, String>("stock_level", StockLevel.ToString()),
                    new KeyValuePair<String, String>("supplier_1", Supplier1.ToString()),
                    new KeyValuePair<String, String>("supplier_1_min_qty", Supplier1MinimumQty.ToString()),
                    new KeyValuePair<String, String>("supplier_1_price", Supplier1Price.ToString()),
                    new KeyValuePair<String, String>("supplier_1_partnumber", Supplier1PartNumber),
                    new KeyValuePair<String, String>("supplier_2", Supplier2.ToString()),
                    new KeyValuePair<String, String>("supplier_2_min_qty", Supplier2MinimumQty.ToString()),
                    new KeyValuePair<String, String>("supplier_2_price", Supplier2Price.ToString()),
                    new KeyValuePair<String, String>("supplier_2_partnumber", Supplier2PartNumber),
                });

                HttpResponseMessage Response = await client.PutAsync("parts/" + StockNumber, PartData);

                Object ResponseBody = JsonConvert.DeserializeObject(await Response.Content.ReadAsStringAsync());

                return (Response.StatusCode, ResponseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return (0, null);
            }
        }

        public async Task<(HttpStatusCode, Object)> DeletePart(int StockNumber)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("parts/" + StockNumber);
                Object ResponseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                return (response.StatusCode, ResponseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return (0, null);
            }
        }

        public async Task<List<Part>> GetParts()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("parts");
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //string ResponseBody = await response.Content.ReadAsStringAsync();
                    List<Part> pl = null;
                    pl = JsonConvert.DeserializeObject<List<Part>>(await response.Content.ReadAsStringAsync());
                    return pl;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return null;
            }
        }

        public async Task<Part> GetPart()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("parts/1");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string ResponseBody = await response.Content.ReadAsStringAsync();
                    Part DecodedJSON = JsonConvert.DeserializeObject<Part>(ResponseBody);
                    return DecodedJSON;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return null;
            }
        }

        public async Task<List<Supplier>> GetSuppliers()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("suppliers");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Supplier> SupplierList = null;
                    SupplierList = JsonConvert.DeserializeObject<List<Supplier>>(await response.Content.ReadAsStringAsync());
                    return SupplierList;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message);
                return null;
            }
        }

        public void PrinterInitialise()
        {
            if (PrinterEnabled)
            {
                LabelPrinter.Interface = Printer.InterfaceType.COM;
                LabelPrinter.COMPort = ConfigurationManager.AppSettings["printer_port"];
                LabelPrinter.COMSetting.Baudrate = Convert.ToInt32(ConfigurationManager.AppSettings["printer_baud"]);
                LabelPrinter.COMSetting.Parameters = ConfigurationManager.AppSettings["printer_parameters"];
            }
        }

        public void PrintLabel(String ManufacturerPartNumber, String PartDescription, String PartManufacturer, String PartNumber)
        {
            if (PrinterEnabled)
            {
                Dictionary<string, string> LabelVariables = new Dictionary<string, string>();
                LabelVariables.Add("@MfgPn@", ManufacturerPartNumber);
                LabelVariables.Add("@PartDescription@", PartDescription);
                LabelVariables.Add("@PartMfg@", PartManufacturer);
                LabelVariables.Add("@PartNumber@", PartNumber);

                string PrinterData = Utils.CommandDataReplace(AppDomain.CurrentDomain.BaseDirectory + "48x35_PartNumberLabel.prn", LabelVariables);

                LabelPrinter.Send(Encoding.Unicode.GetBytes(PrinterData));
            }
        }
    }
}
