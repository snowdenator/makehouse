using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PartsDB.Types
{
    public class PartList
    {
        public Part[] part { get; set; }
    }
    public class Part
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }
        [JsonProperty("mfg_partnumber")]
        public string ManufacturerPartNumber { get; set; }
        [JsonProperty("stock_level")]
        public int StockLevel { get; set; }
        [JsonProperty("stock_number")]
        public int StockNumber { get; set; }
        [JsonProperty("preferred_supplier")]
        public Supplier PreferredSupplier { get; set; }
        [JsonProperty("supplier_1")]
        public Supplier Supplier1 { get; set; }
        [JsonProperty("supplier_1_min_qty")]
        public int Supplier1MinimumQty { get; set; }
        [JsonProperty("supplier_1_pn")]
        public string Supplier1PartNumber { get; set; }
        [JsonProperty("supplier_1_price")]
        public double Supplier1Price { get; set; }
        [JsonProperty("supplier_2")]
        public Supplier Supplier2 { get; set; }
        [JsonProperty("supplier_2_min_qty")]
        public int Supplier2MinimumQty { get; set; }
        [JsonProperty("supplier_2_pn")]
        public string Supplier2PartNumber { get; set; }
        [JsonProperty("supplier_2_price")]
        public double Supplier2Price { get; set; }
    }

    public class Supplier
    {
        [JsonProperty("supplier_contact")]
        public string Contact { get; set; }
        [JsonProperty("supplier_name")]
        public string Name { get; set; }
        [JsonProperty("supplier_number")]
        public int Number { get; set; }
        [JsonProperty("supplier_website")]
        public string Website { get; set; }
    }
}
