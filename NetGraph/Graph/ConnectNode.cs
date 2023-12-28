namespace CyConex.Graph
{
    internal class ConnectNode
    {
        string nodeID;
        string edgeSourceNodeID;
        string edgeTargetNodeID;
        string edgeTitle;
        string edgeDescription;
        string edgeRelationship;
        string edgeStrength;
        string edgeStrengthValue;
        string edgeStrengthMinValue;
        string edgeStrengthDistribution;


        public string NodeID
        {
            get { return nodeID; }
            set { nodeID = value; }
        }
        
        public string EdgeSourceNodeID
        {
            get { return edgeSourceNodeID; }
            set { edgeSourceNodeID = value; }
        }

        public string EdgeTargetNodeID
        {
            get { return edgeTargetNodeID; }
            set { edgeTargetNodeID = value; }
        }

        public string EdgeTitle
        {
            get { return edgeTitle; }
            set { edgeTitle = value; }
        }

        public string EdgeDescription
        {
            get { return edgeDescription; }
            set { edgeDescription = value; }
        }

        public string EdgeRelationship
        {
            get { return edgeRelationship; }
            set { edgeRelationship = value; }
        }

        public string EdgeStrength
        {
            get { return edgeStrength; }
            set { edgeStrength = value; }
        }

        public string EdgeStrengthValue
        {
            get { return edgeStrengthValue; }
            set { edgeStrengthValue = value; }
        }

        public string EdgeStrengthMinValue
        {
            get { return edgeStrengthMinValue; }
            set { edgeStrengthMinValue = value; }
        }

        public string EdgeStrengthDistribution
        {
            get { return edgeStrengthDistribution; }
            set { edgeStrengthDistribution = value; }
        }

    }
}
