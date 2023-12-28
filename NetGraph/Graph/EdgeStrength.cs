namespace CyConex.Graph
{
    internal class EdgeStrength
    {
        string assetStatus;
        string assetValue;
        string assetDescription;

        public string AssetStatus
        {
            get { return assetStatus; }
            set { assetStatus = value; }
        }

        public string AssetValue
        {
            get { return assetValue; }
            set { assetValue = value; }
        }

        public string AssetDescription
        {
            get { return assetDescription; }
            set { assetDescription = value; }
        }

        public EdgeStrength(string assetStatus, string assetValue, string assetDescription)
        {
            this.AssetStatus = assetStatus;
            this.AssetValue = assetValue;
            this.AssetDescription = assetDescription;
        }
    }
}
