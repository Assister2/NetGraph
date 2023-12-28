namespace CyConex.Graph
{
    internal class EdgeNode
    {
        string nodeTitle;
        string nodeType;
        string nodeDescription;
        string nodeID;

        public string NodeID
        {
            get { return nodeID; }
            set { nodeID = value; }
        }

        public string NodeTitle
        {
            get { return nodeTitle; }
            set { nodeTitle = value; }
        }

        public string NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        public string NodeDescription
        {
            get { return nodeDescription; }
            set { nodeDescription = value; }
        }
        public EdgeNode(string id, string title, string type, string description)
        {
            this.nodeID = id;
            this.nodeTitle = title;
            this.nodeType = type;
            this.nodeDescription = description;
        }
    }
}
