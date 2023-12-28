namespace CyConex.Graph
{
    public class NodeShape
	{
		public string Shape { get; set; }
		public string Name { get; set; }

		public NodeShape(string shape, string name)
		{
			Shape = shape;
			Name = name;
		}

		public new string ToString()
		{
			return Name;
		}

	}
}
