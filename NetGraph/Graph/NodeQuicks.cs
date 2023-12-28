using System.Collections.Generic;

namespace CyConex.Graph
{
    internal class NodeQuicks
    {
		public static List<NodeQuick> NodesQuicks = new List<NodeQuick>()
		{
			new NodeQuick("control", "Control"),
			new NodeQuick("group", "Group"),
			new NodeQuick("asset", "Asset"),
			new NodeQuick("objective", "Objective"),
			new NodeQuick("attack", "Attack"),
			new NodeQuick("actor", "Actor"),
            new NodeQuick("actor", "Actor"),
            new NodeQuick("vulnerability", "Vulnerability")
		};
	}
}
