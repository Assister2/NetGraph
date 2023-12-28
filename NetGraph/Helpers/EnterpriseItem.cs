using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex
{
    public class EnterpriseItem
    {
        [JsonProperty("enterpriseGUID")]
        public string EnterpriseGUID { get; set; }

        [JsonProperty("enterpriseName")]
        public string EnterpriseName { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        public EnterpriseItem() { }

        public EnterpriseItem(string guid, string name, string line1, string line2, string pcode, string city, string state, string country)
        {
            this.EnterpriseGUID = guid;
            this.EnterpriseName = name;
            this.AddressLine1 = line1;
            this.AddressLine2 = line2;
            this.Postcode = pcode;
            this.City = city;
            this.State = state;
            this.Country = country;
        }
    }
}
