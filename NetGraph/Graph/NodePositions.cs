using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;

namespace CyConex.Graph
{
    internal class NodePositions
	{
		public string ID { get; set; }
		public string Title { get; set; }
		public Location GraphLocation { get; set; }
		public Point ScreenLocation { get; set; }

		public NodePositions()
		{
			ID = String.Empty;
			Title = String.Empty;
			GraphLocation = new Location(0, 0);
			ScreenLocation = new Point(0, 0);
		}

		internal static NodePositions FromJson(string data)
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() {
				Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
				{
					Debug.WriteLine($"Error");
					//errors.Add(args.ErrorContext.Error.Message);
					args.ErrorContext.Handled = false;
				},
			};
			return JsonConvert.DeserializeObject<NodePositions>(data);
		}
		
	}
}
