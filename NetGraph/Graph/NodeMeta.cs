using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.Graph
{
    public class NodeMeta
    {
        //[JsonProperty("GUID")]
        //public string GUID { get; set; }

        [JsonProperty("CreatedDate")]
        public string createdDate { get; set; }

        [JsonProperty("NodeType")]
        public string nodeType { get; set; }

        [JsonProperty("Category")]
        public string category { get; set; }

        [JsonProperty("SubCategory")]
        public string subCatagory { get; set; }

        public NodeMeta() { }

        public NodeMeta(string createdDate, string nodeType, string category, string subCategory, string guid = "")
        {
            //this.GUID = guid;
            this.createdDate = createdDate;
            this.nodeType = nodeType;
            this.category = category;
            this.subCatagory = subCategory;
        }
    }
}
