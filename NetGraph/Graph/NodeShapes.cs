using System.Collections.Generic;

namespace CyConex.Graph
{
    public static class NodeShapes
	{
		//We do it in this way for support custom nodes in future
		public static List<NodeShape> NodesShapes = new List<NodeShape>()
		{
			new NodeShape("ellipse", "Ellipse"),
			new NodeShape("triangle", "Triangle"),
			new NodeShape("round-triangle", "Round triangle"),
			new NodeShape("rectangle", "Rectangle"),
			new NodeShape("round-rectangle", "Round rectangle"),
			new NodeShape("bottom-round-rectangle", "Bottom Round rectangle"),
			new NodeShape("cut-rectangle", "Cut rectangle"),
			new NodeShape("barrel", "Barrel"),
			new NodeShape("rhomboid", "Rhomboid"),
			new NodeShape("diamond", "Diamond"),
			new NodeShape("round-diamond", "Round diamond"),
			new NodeShape("pentagon", "Pentagon"),
			new NodeShape("round-pentagon", "Round pentagon"),
			new NodeShape("hexagon", "Hexagon"),
			new NodeShape("round-hexagon", "Round hexagon"),
			new NodeShape("concave-hexagon", "Concave hexagon"),
			new NodeShape("heptagon", "Heptagon"),
			new NodeShape("round-heptagon", "Round heptagon"),
			new NodeShape("octagon", "Octagon"),
			new NodeShape("round-octagon", "Round octagon"),
			new NodeShape("star", "Star"),
			new NodeShape("tag", "Tag"),
			new NodeShape("round-tag", "Round tag"),
			new NodeShape("vee", "Vee")
		};

	}
}
