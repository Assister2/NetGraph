namespace CyConex.Graph
{
    internal class NodeQuick
    {
		public string Quick { get; set; }
		public string Name { get; set; }

		public NodeQuick(string quick, string name)
		{
			Quick = quick;
			Name = name;
		}

		public new string ToString()
		{
			return Name;
		}
	}
}
