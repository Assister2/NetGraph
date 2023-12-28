using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using CyConex.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CyConex.Graph
{
    public class Edge
	{

		public string ID { get; set; }

		public string Source { get; set; }

		public string Target { get; set; }
		public double Weight { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Note { get; set; }

		public string Relationship { get; set; }

		public string RelationshipStrength { get; set; }

		public bool Enabled { get; set; }

		public double DrawingWeight { get; set; }

		public double ImpactedValue { get; set; }

		public System.Drawing.Color Color { get; set; }
		
		public double LabelSize { get; set; }

		public override string ToString()
		{
			return Title;
		}
        public string edgeStrengthValue { get; set; }
        public string edgeStrengthMinValue { get; set; }
        public string edgeStrengthDistribution { get; set; }

        public static Edge FromJson(string json)
		{
			try
			{
				var edgeJson = JObject.Parse(json);
				var edgeData = edgeJson["data"];
				if (edgeData == null)
				{
					edgeData = edgeJson;
				}
				Edge retval = new Edge()
				{
					ID = edgeData["id"].ToString(),
					Source = edgeData["source"].ToString(),
					Target = edgeData["target"].ToString(),
					Weight = Convert.ToDouble(edgeData["weight"].ToString(), CultureInfo.InvariantCulture),
					Title = edgeData["title"].ToString(),
					Description = edgeData["description"].ToString(),
					Note = edgeData["note"].ToString(),
					Relationship = edgeData["relationship"].ToString(),
					LabelSize = Convert.ToDouble(edgeData["labelSize"].ToString()),
					Enabled = edgeData["enabled"].ToString().ToLower() == "true",
					DrawingWeight = (edgeData["edgeStrengthValue"] == null ? Convert.ToDouble("0.0", CultureInfo.InvariantCulture) : Convert.ToDouble(edgeData["edgeStrengthValue"].ToString(), CultureInfo.InvariantCulture)),
                    ImpactedValue = (edgeData["impactedValue"] == null ? Convert.ToDouble("0.0", CultureInfo.InvariantCulture) : Convert.ToDouble(edgeData["impactedValue"].ToString(), CultureInfo.InvariantCulture)),
					Color = (GeneralHelpers.ConvertColorFromHTML(edgeData["color"].ToString())),
					edgeStrengthValue = edgeData["edgeStrengthValue"] == null ? "" : edgeData["edgeStrengthValue"].ToString(),
                    edgeStrengthMinValue = edgeData["edgeStrengthMinValue"] == null ? "" : edgeData["edgeStrengthMinValue"].ToString(),
                    edgeStrengthDistribution = edgeData["edgeStrengthDistribution"] == null ? "" : edgeData["edgeStrengthDistribution"].ToString(),
                };
				return retval;
			}
			catch 
			{
                return null;

            }
		}

		public static Edge FromDictionary(IDictionary<String, Object> jsonDict)
		{
			Edge retval = new Edge()
			{
				Source = jsonDict["source"].ToString(),
				Target = jsonDict["target"].ToString(),
				Weight = Convert.ToDouble(jsonDict["weight"].ToString(), CultureInfo.InvariantCulture),
				ID = jsonDict["id"].ToString(),
				Title = jsonDict["title"].ToString(),
				Description = jsonDict["description"].ToString(),
				Note = jsonDict["note"].ToString(),
				Relationship = jsonDict["relationship"].ToString(),
				LabelSize = Convert.ToDouble(jsonDict["labelSize"].ToString()),
				Enabled = jsonDict["enabled"].ToString().ToLower() == "true",
				DrawingWeight = Convert.ToDouble(jsonDict["edgeStrengthValue"].ToString(), CultureInfo.InvariantCulture),
				ImpactedValue = Convert.ToDouble(jsonDict["impactedValue"].ToString(), CultureInfo.InvariantCulture),
				Color = GeneralHelpers.ConvertColorFromHTML(jsonDict["color"].ToString()),
				edgeStrengthValue = jsonDict["edgeStrengthValue"].ToString(),
			};
			return retval;
		}

        public Edge Clone()
        {
            return (Edge)this.MemberwiseClone();
        }
    }
}
