namespace CyConex.Graph
{
    public class NodeType
	{
		public string Type { get; set; }
		public string Name { get; set; }

		public NodeType(string type, string name)
		{
			Type = type;
			Name = name;
		}

		public new string ToString()
		{
			return Name;
		}

	}
}
