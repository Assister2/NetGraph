using System.Collections.Generic;

namespace CyConex.Graph
{
    public static class NodeTypes
    {
        //We do it in this way for support custom nodes in future
        public static List<NodeType> NodesTypes = new List<NodeType>()
        {
            new NodeType("control", "Control"),
            new NodeType("group", "Group"),
            new NodeType("asset", "Asset"),
            new NodeType("asset-group", "Asset-Group"),
            new NodeType("info", "Info"),
            new NodeType("objective", "Objective"),
            new NodeType("attack", "Attack"),
            new NodeType("actor", "Actor"),
            new NodeType("vulnerability", "Vulnerability"),
            new NodeType("evidence", "Evidence"),
            new NodeType("vulnerability-group", "Vulnerability-Group")
        };

    }
}
