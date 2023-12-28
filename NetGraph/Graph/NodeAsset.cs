namespace CyConex.Graph
{
    internal class NodeAsset
    {
        string assetStatus;
        string assetValue;
        string assetDescription;
        string color;

        public string Strength
        {
            get { return assetStatus; }
            set { assetStatus = value; }
        }

        public string Value
        { 
            get { return assetValue; } 
            set { assetValue = value; } 
        }

        public string Description
        { 
            get { return assetDescription; } 
            set { assetDescription = value; } 
        }

        /*public string Color
        {
            get { return color; }
            set { color = value; }
        }*/

        public NodeAsset(string assetStatus, string assetValue, string assetDescription/*, string color = "RGB(0,0,0)"*/)
        {
            this.Strength = assetStatus;
            this.Value = assetValue;
            this.Description = assetDescription;
            //this.color = color;
        }
    }
}