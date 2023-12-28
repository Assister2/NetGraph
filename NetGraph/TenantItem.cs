using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex
{
    public class TenantItem
    {
        [JsonProperty("tenantGUID")]
        public string TenantGUID { get; set; }

        [JsonProperty("TenantName")]
        public string TenantName { get; set; }

        [JsonProperty("TenantDescription")]
        public string TenantDescription { get; set; }

        public TenantItem() { }

        public TenantItem(string guid, string name, string description)
        {
            this.TenantGUID = guid;
            this.TenantName = name;
            this.TenantDescription = description;
        }
    }
}
