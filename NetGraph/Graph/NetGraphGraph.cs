using System;

namespace CyConex.Graph
{
    internal class NetGraphGraphV2
	{
		public GraphProperties GraphProperties { get; set; }

		object Graph { get; set; }

		public NetGraphGraphV2() : base()
		{
			GraphProperties = new GraphProperties()
			{
				Created = DateTime.Now
			};
			
		}

	}

	//internal class NetGraphGraph : BidirectionalGraph<DataVertex, DataEdge> 
	//{
	//	public Dictionary<DataVertex, Point> VertexLocations;

	//	public GraphProperties GraphProperties { get; set; }

	//	public NetGraphGraph(): base()
	//	{
	//		GraphProperties = new GraphProperties()
	//		{
	//			Created = DateTime.Now
	//		};
	//		VertexLocations = new Dictionary<DataVertex, Point>();
	//	}
		
	//	/// <summary>
	//	/// Return selected node
	//	/// </summary>
	//	public DataVertex SelectedVertex
	//	{
	//		get 
	//		{ 
	//			return this.Vertices.FirstOrDefault(item => item.Selected); 
	//		}
	//	}
	//}

}
